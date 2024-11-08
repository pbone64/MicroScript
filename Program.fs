module Program

open Tokens

module Lex =
    let peekAt (src: string) (offset: int): option<char> =
        if offset >= src.Length then None else Some(src[offset])

    let peekNext (src: string) (offset: int): option<char> =
        peekAt src (offset + 1)

    let nextIs (src: string) (offset: int) (query: char): bool =
        match peekNext src offset with
        | Some(c) -> c = query
        | None -> false

    type private munchUntil_State =
        | Continue
        | Stop
        | Fail

    let munchWhile (src: string) (offset: int) (condition: char -> bool): string = ([|
        let mutable n = offset
        let mutable state = Continue

        while state = Continue do
            let next = peekAt src n
            match next with
            | Some(c) -> if condition c then yield c else state <- Stop
            | None -> state <- Fail

            n <- (n + 1)
    |] |> System.String)

    // TODO count column number
    let parseOne (src: string) (offset: int): Token =
        match src[offset] with
        | '(' -> LParen
        | ')' -> RParen
        | '{' -> LBrace
        | '}' -> RBrace
        | '.' -> Dot
        | ',' -> Comma
        | ':' -> Colon
        | '+' -> Plus
        | '-' -> Minus
        | '*' -> Star
        | '/' -> Slash
        | '=' -> if nextIs src offset '=' then EqEq else Eq
        | '>' -> if nextIs src offset '=' then GtEq else Gt
        | '<' -> if nextIs src offset '=' then LtEq else Lt
        | '!' -> if nextIs src offset '=' then BangEq else Bang
        | '#' -> Comment
        | ' ' | '\t' -> Whitespace
        | '\n' | ';' -> Delim // TODO count line number
        | '"' -> StringLiteral(munchWhile src (offset + 1) (fun c -> c <> '"')) // TODO report unended strings
        | _ as c ->
            // TODO these string casts probably generate a lot of garbage
            if digits.IsMatch(string c) then
                let word =
                    munchWhile src offset (fun c -> digits.IsMatch(string c))

                let value = int word

                IntLiteral(value)
            else if identStartChars.IsMatch(string c) then
                let word =
                    munchWhile src offset (fun c -> identChars.IsMatch(string c))

                let isKeyword, kwToken = Token.keywords.TryGetValue(word)

                if isKeyword then kwToken else Ident(word)
            else
                Soup(c)


    let lex (src: string): list<Token> = [
        let mutable n = 0

        while n < src.Length do
            let token = parseOne src n

            if token = Comment then
                while n < src.Length && not (nextIs src n '\n') do n <- n + 1
            else
                if token <> Whitespace then yield token
                n <- n + token.srcLength

        yield EOF
    ]

[<EntryPoint>]
let main argv =
    while true do
        printf "> "
        let input = System.Console.ReadLine()
        printfn "%A" (Lex.lex input)
    0