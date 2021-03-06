namespace PQTool

open Elmish
open Avalonia
open Avalonia.FuncUI.Components.Hosts
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI

type MainWindow() as this =
    inherit HostWindow()

    do
        base.Title <- "PQ Formula Calculator"
        base.Width <- 600.0
        base.Height <- 400.0
        base.Background <- Media.Brush.Parse("white")
        base.CanResize <- false
        base.WindowState <- Controls.WindowState.Normal
        
        Elmish.Program.mkSimple (fun() -> Main.init) Main.Update Main.View
        |> Avalonia.FuncUI.Elmish.Program.withHost this
        |> Program.run
        
type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Load "avares://Avalonia.Themes.Default/DefaultTheme.xaml"
        this.Styles.Load "avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()

        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        try
            AppBuilder.Configure<App>().UsePlatformDetect().UseSkia().StartWithClassicDesktopLifetime(args)
        with
            _ ->
                -1
