open System

let read_key() : double =
    let ok, result = Double.TryParse(Console.ReadLine())
    if ok then
        result
    else
        raise(Exception("Use numbers only"))

let PQ p q =
    if (Math.Pow(p/2.0, 2.0) - q) < 0.0 then
        raise(Exception("Error"))

    let temp = [| -(p/2.0) + Math.Sqrt(Math.Pow(p/2.0, 2.0) - q), -(p/2.0) - Math.Sqrt(Math.Pow(p/2.0, 2.0) - q)|]
    temp

[<EntryPoint>]
let main argv =
    printf "Value for P: "
    let p1 : double = read_key()

    printf "Value for Q: "
    let q1 : double = read_key()

    let vol = PQ p1 q1
    printf "\nf(x) = x² + p + q\n"
    printf "x1 = %A; x2 = %A" (fst vol.[0]) (snd vol.[0])
    0
