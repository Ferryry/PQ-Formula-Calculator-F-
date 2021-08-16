open System

let PQ p q =
    if Math.Sqrt(Math.Pow(p/2.0, 2.0) - q) < 0.0 then
        raise(Exception("Error"))

    let temp = [| -(p/2.0) + Math.Sqrt(Math.Pow(p/2.0, 2.0) - q), -(p/2.0) - Math.Sqrt(Math.Pow(p/2.0, 2.0) - q)|]
    temp

[<EntryPoint>]
let main argv =
    printf "Value for P: "
    let p1 : double = Double.Parse(Console.ReadLine())
    printf "Value for Q: "
    let q1 : double = Double.Parse(Console.ReadLine())

    let vol = PQ p1 q1
    printf "\nf(x) = x² + p + q\n"
    printf "x1 = %A; x2 = %A" (fst vol.[0]) (snd vol.[0])
    0
