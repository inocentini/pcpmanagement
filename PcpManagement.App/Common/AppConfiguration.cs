using MudBlazor;
using MudBlazor.Utilities;

namespace PcpManagement.App.Common;

public static class AppConfiguration
{
    public const string HttpClientName = "PcpManagement.Api";
    public const string BackendUrl = "http://localhost:5148";
    
    public static readonly MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = new[] { "Raleway", "sans-serif" }
            }
        },
        PaletteLight = new PaletteLight
        {
            Black = "#1A1A1A",
            White = "#FFFFFF",
            Primary = "#662D91", // Vivo Purple
            PrimaryContrastText = "#FFFFFF",
            Secondary = "#0098DB", // Vivo Blue
            SecondaryContrastText = "#FFFFFF",
            Tertiary = "#A0C814", // Vivo Green
            TertiaryContrastText = "#1A1A1A",
            Info = "#0098DB", // Vivo Blue (usado também como Info)
            InfoContrastText = "#FFFFFF",
            Success = "#A0C814", // Vivo Green
            SuccessContrastText = "#1A1A1A",
            Warning = "#FFC20E", // Vivo Yellow
            WarningContrastText = "#1A1A1A",
            Error = "#E1001A", // Vivo Red
            ErrorContrastText = "#FFFFFF",
            Dark = "#1A1A1A", // Vivo Black
            DarkContrastText = "#FFFFFF",
            TextPrimary = "#1A1A1A",
            TextSecondary = "#662D91",
            TextDisabled = "#C4C4C4",
            ActionDefault = "#1A1A1A",
            ActionDisabled = "#C4C4C4",
            ActionDisabledBackground = "#F5F5F5",
            Background = "#FFFFFF",
            BackgroundGray = "#F5F5F5",
            Surface = "#FFFFFF",
            DrawerBackground = "#662D91",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#FFFFFF",
            AppbarBackground = "#662D91",
            AppbarText = "#FFFFFF",
            LinesDefault = "#1A1A1A",
            LinesInputs = "#662D91",
            TableLines = "#E0E0E0",
            TableStriped = "#F5F5F5",
            TableHover = "#F0F0F0",
            Divider = "#E0E0E0",
            DividerLight = "#F0F0F0",
            PrimaryDarken = "#4A1C6E",
            PrimaryLighten = "#8B4FC1",
            SecondaryDarken = "#006EA3",
            SecondaryLighten = "#33ACFF",
            TertiaryDarken = "#788A0F",
            TertiaryLighten = "#BBDD48",
            InfoDarken = "#006EA3",
            InfoLighten = "#33ACFF",
            SuccessDarken = "#788A0F",
            SuccessLighten = "#BBDD48",
            WarningDarken = "#B5880A",
            WarningLighten = "#FFD748",
            ErrorDarken = "#A10014",
            ErrorLighten = "#FF3338",
            DarkDarken = "#0F0F0F",
            DarkLighten = "#404040",
            HoverOpacity = 0.1,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.1,
            GrayDefault = "#F5F5F5",
            GrayLight = "#E0E0E0",
            GrayLighter = "#F0F0F0",
            GrayDark = "#BDBDBD",
            GrayDarker = "#9E9E9E",
            OverlayDark = "rgba(0, 0, 0, 0.5)",
            OverlayLight = "rgba(255, 255, 255, 0.5)"
        },
        PaletteDark = new PaletteDark
        {
            Black = "#1A1A1A",
            White = "#FFFFFF",
            Primary = "#662D91",
            PrimaryContrastText = "#FFFFFF",
            Secondary = "#0098DB",
            SecondaryContrastText = "#FFFFFF",
            Tertiary = "#A0C814",
            TertiaryContrastText = "#1A1A1A",
            Info = "#0098DB",
            InfoContrastText = "#FFFFFF",
            Success = "#A0C814",
            SuccessContrastText = "#1A1A1A",
            Warning = "#FFC20E",
            WarningContrastText = "#1A1A1A",
            Error = "#E1001A",
            ErrorContrastText = "#FFFFFF",
            Dark = "#1A1A1A",
            DarkContrastText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#F5F5F5",
            TextDisabled = "#9E9E9E",
            ActionDefault = "#FFFFFF",
            ActionDisabled = "#9E9E9E",
            ActionDisabledBackground = "#4A4A4A",
            Background = "#1A1A1A",
            BackgroundGray = "#424242",
            Surface = "#333333",
            DrawerBackground = "#662D91",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#FFFFFF",
            AppbarBackground = "#662D91",
            AppbarText = "#FFFFFF",
            LinesDefault = "#E0E0E0",
            LinesInputs = "#8B4FC1",
            TableLines = "#555555",
            TableStriped = "#2A2A2A",
            TableHover = "#3A3A3A",
            Divider = "#444444",
            DividerLight = "#666666",
            PrimaryDarken = "#4A1C6E",
            PrimaryLighten = "#8B4FC1",
            SecondaryDarken = "#006EA3",
            SecondaryLighten = "#33ACFF",
            TertiaryDarken = "#788A0F",
            TertiaryLighten = "#BBDD48",
            InfoDarken = "#006EA3",
            InfoLighten = "#33ACFF",
            SuccessDarken = "#788A0F",
            SuccessLighten = "#BBDD48",
            WarningDarken = "#B5880A",
            WarningLighten = "#FFD748",
            ErrorDarken = "#A10014",
            ErrorLighten = "#FF3338",
            DarkDarken = "#0F0F0F",
            DarkLighten = "#404040",
            HoverOpacity = 0.1,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.1,
            GrayDefault = "#333333",
            GrayLight = "#424242",
            GrayLighter = "#555555",
            GrayDark = "#BDBDBD",
            GrayDarker = "#9E9E9E",
            OverlayDark = "rgba(0, 0, 0, 0.5)",
            OverlayLight = "rgba(255, 255, 255, 0.5)"
        }
    };

}