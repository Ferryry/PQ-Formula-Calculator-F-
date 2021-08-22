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
            raise(Exception("Error"))

        let temp = [| -(p/2.0) + Math.Sqrt(Math.Pow(p/2.0, 2.0) - q), -(p/2.0) - Math.Sqrt(Math.Pow(p/2.0, 2.0) - q) |]
        temp

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

                    { state with Result = (sprintf "%A" result) }
                | _ ->
                    raise (Exception "inputs are not valid floats")
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
                        ]
                        TextBox.create [
                            TextBox.width 128.0
                            TextBox.dock Dock.Bottom
                            TextBox.background "#ffffff"
                            TextBox.onTextChanged (fun input -> dispatch (UpdateP input))
                            TextBox.horizontalAlignment HorizontalAlignment.Stretch
                        ]
                        TextBlock.create [
                            TextBlock.text "Q: "
                            TextBlock.foreground "black"
                            TextBlock.fontSize 24.0
                        ]
                        TextBox.create [
                            TextBox.width 128.0
                            TextBox.dock Dock.Bottom
                            TextBox.background "#ffffff"
                            TextBox.onTextChanged (fun input -> dispatch (UpdateQ input))
                            TextBox.horizontalAlignment HorizontalAlignment.Stretch
                        ]
                        Button.create [
                            Button.content "Calculate"
                            Button.dock Dock.Bottom
                            Button.onClick (fun _ -> dispatch Calc)
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
