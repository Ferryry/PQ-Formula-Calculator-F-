namespace PQTool

module Main =
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    open System
    
    type State = {
        QInput : string
        PInput : string
        Result : string
    }

    let init = {
        QInput = ""
        PInput = ""
        Result = ""
    }

    let GetResult p q =
        if Math.Pow(p/2.0, 2.0) - q < 0.0 then
            None
        else
            let temp = [| -(p/2.0) + Math.Sqrt(Math.Pow(p/2.0, 2.0) - q), -(p/2.0) - Math.Sqrt(Math.Pow(p/2.0, 2.0) - q) |]
            Some temp

    type Msg =
        | Calc
        | UpdateQ of string
        | UpdateP of string

    let Update (msg: Msg) (state: State) : State =
        match msg with
            | Calc ->
                let qOk, qValue = Double.TryParse state.QInput
                let pOk, pValue = Double.TryParse state.PInput

                match pOk, qOk with
                | true, true ->
                    let result = GetResult qValue pValue

                    match result with
                    | Some value ->
                        { state with Result = (sprintf "%A" value) }
                    | None ->
                        { state with Result = "There are no zeros available in your given\nfunction of f(x) = x² + px + q" }
                | _ ->
                    { state with Result = "Error" }
            | UpdateP input -> { state with QInput = input  }
            | UpdateQ input -> { state with PInput = input  }

    let View (state: State) (dispatch: Msg -> unit) =
        StackPanel.create [
            StackPanel.children [
                WrapPanel.create [
                    WrapPanel.children [
                        TextBlock.create [
                            TextBlock.text "P: "
                            TextBlock.foreground "black"
                            TextBlock.fontSize 24.0
                            TextBlock.margin (0.8, 0.0, 0.0, 0.0)
                        ]
                        TextBox.create [
                            TextBox.width 200.0
                            TextBox.background "#ffffff"
                            TextBox.onTextChanged (fun input -> dispatch (UpdateP input))
                            TextBox.horizontalAlignment HorizontalAlignment.Stretch
                            TextBox.fontSize 20.0
                            TextBox.foreground "black"
                        ]
                        TextBlock.create [
                            TextBlock.text "Q: "
                            TextBlock.foreground "black"
                            TextBlock.fontSize 24.0
                            TextBlock.margin (32.0, 0.0, 8.0, 0.0)
                        ]
                        TextBox.create [
                            TextBox.width 200.0
                            TextBox.background "#ffffff"
                            TextBox.onTextChanged (fun input -> dispatch (UpdateQ input))
                            TextBox.horizontalAlignment HorizontalAlignment.Stretch
                            TextBox.fontSize 20.0
                            TextBox.foreground "black"
                        ]
                        Button.create [
                            Button.content "Calculate"
                            Button.dock Dock.Bottom
                            Button.onClick (fun _ -> dispatch Calc)
                            Button.width 71.0
                            Button.horizontalAlignment HorizontalAlignment.Stretch
                            TextBlock.margin (32.0, 0.0, 0.0, 0.0)
                        ]
                    ]
                ]
                WrapPanel.create [
                    WrapPanel.children [
                        TextBlock.create [
                            TextBlock.text (sprintf "%A" state.Result)
                            TextBlock.foreground "black"
                            TextBlock.fontSize 24.0
                        ]
                    ]
                ]
            ]
        ]
