using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Api.Services;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Tests;

public class VirtualMachineServiceTests(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("PcpManagement.Api");
    
    [Fact]
    public async Task GetAllWithoutAssociationAsync_ReturnsPagedResponseWithVms()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RpaContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        await using var context = new RpaContext(options);
        context.Vms.AddRange(
            new Vm { Hostname = "VM1", Ip = "192.168.1.1", UserVm = "user1", VCpu = 2, Memoria = 4, Hd = 100, Ambiente = "Dev", Emprestimo = false, Resolucao = "1920x1080", Enviroment = "Test", SistemaOperacional = "Windows", Status = true, Observacao = "None", Funcionalidade = "Test", Lote = DateTime.Now, DataCenter = "DC1", Farm = "Farm1" },
            new Vm { Hostname = "VM2", Ip = "192.168.1.2", UserVm = "user2", VCpu = 4, Memoria = 8, Hd = 200, Ambiente = "Prod", Emprestimo = false, Resolucao = "1920x1080", Enviroment = "Test", SistemaOperacional = "Linux", Status = true, Observacao = "None", Funcionalidade = "Test", Lote = DateTime.Now, DataCenter = "DC2", Farm = "Farm2" }
        );
        await context.SaveChangesAsync();

        var service = new VirtualMachineService(context);

        var request = new GetAllVirtualMachineWithouAssociationRequest { PageNumber = 1, PageSize = 10 };

        // Act
        var response = await service.GetAllWithoutAssociationAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(2, response.Data!.Count);
        Assert.Equal(1, response.Data[0].Id);
        Assert.Equal("VM1", response.Data[0].Hostname);
    }

    [Fact]
    public async Task GetAllWithoutAssociationAsync_ReturnsEmptyListWhenNoVms()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RpaContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new RpaContext(options);

        var service = new VirtualMachineService(context);

        var request = new GetAllVirtualMachineWithouAssociationRequest { PageNumber = 1, PageSize = 10 };

        // Act
        var response = await service.GetAllWithoutAssociationAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response.Data!);
    }

    [Fact]
    public async Task GetAllWithoutAssociationAsync_ReturnsInternalServerErrorOnException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RpaContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new RpaContext(options);
        var service = new VirtualMachineService(context);

        var request = new GetAllVirtualMachineWithouAssociationRequest { PageNumber = 1, PageSize = 10 };

        // Simulate an exception
        await context.Database.EnsureDeletedAsync();

        // Act
        var response = await service.GetAllWithoutAssociationAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(EStatusCode.InternalServerError, response.Code);
        Assert.Null(response.Data);
    }
    
    
    [Fact]
    public async Task GetAllAsync_ReturnsInternalServerErrorOnException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RpaContext>()
            .UseSqlServer("Server=localhost,1433;Database=rpa;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;")
            .Options;

        using var context = new RpaContext(options);
        var service = new VirtualMachineService(context);

        int pageNumber = 1;
        const int pageSize = 25;
        bool hasMoreResults;
        var allVms = new List<Vm>();

        do
        {
            var request = new GetAllVirtualMachinesRequest { PageNumber = pageNumber, PageSize = pageSize };

            // Act
            //var response = await service.GetAllAsync(request);
            var response = await _httpClient.GetFromJsonAsync<PagedResponse<List<Vm>>>("/v1/virtualmachine?pageNumber={request.PageNumber}&pageSize={request.PageSize}");

            // Assert
            Assert.NotNull(response);
            Assert.NotEqual(EStatusCode.InternalServerError, response.Code);
            
            if (response.Data != null)
            {
                allVms.AddRange(response.Data);
            }

            hasMoreResults = response.Data!.Count > 0;
            pageNumber++;
        } while (hasMoreResults);
        
        Console.WriteLine("Total de páginas: " + pageNumber +" Total de registros: " + allVms.Count);
    }
}