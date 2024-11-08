module Compiler

open Tokens

let compile (tokens: list<Token>) = [
    let mutable n = 0

    while n < tokens.Length do
        let q = tokens[n]

        match q with
        | Var -> 0
        | Struct -> 0
        | Fn -> 0
        | Module -> 0
        | _ -> 0 // ERR
]
