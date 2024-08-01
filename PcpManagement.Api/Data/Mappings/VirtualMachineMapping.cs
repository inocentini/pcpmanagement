using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PcpManagement.Core.Models;

namespace PcpManagement.Api.Data.Mappings;

public class VirtualMachineMapping : IEntityTypeConfiguration<VirtualMachine>
{
    public void Configure(EntityTypeBuilder<VirtualMachine> builder)
    {
        builder.ToTable("VirtualMachines");
    }
}