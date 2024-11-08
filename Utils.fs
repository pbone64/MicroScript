module Utils

[<TailCall>]
let numDigits n =
    let rec loop n =
        if n < 10 then 1 else (1 + loop (n / 10))

    loop n
