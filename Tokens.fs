module Tokens

open Utils
open System.Text.RegularExpressions

type Token =
    | LParen
    | RParen
    | LBrace
    | RBrace

    | Dot
    | Comma
    | Colon

    | Plus
    | Minus
    | Star
    | Slash

    | Eq
    | Gt
    | Lt
    | Bang

    | EqEq
    | GtEq
    | LtEq
    | BangEq

    | Var
    | Struct
    | Fn
    | Module

    | If
    | Ident of string

    | IntLiteral of int32
    | BoolLiteral of bool
    | StringLiteral of string

    | Delim
    | Comment
    | Whitespace
    | Soup of char
    | EOF

    member this.srcLength: int =
        match this with
        | Var -> "var".Length
        | Struct -> "struct".Length
        | Fn -> "fn".Length
        | Module -> "module".Length
        | If -> "if".Length
        | Ident(s) -> s.Length
        | IntLiteral(i) -> numDigits i
        | BoolLiteral(b) -> if b then "true".Length else "false".Length
        | StringLiteral(s) -> s.Length + 2
        | EqEq | GtEq | LtEq | BangEq -> 2
        | EOF -> 0
        | _ -> 1

    static member keywords = Map [
        "var", Var;
        "struct", Struct;
        "fn", Fn;
        "module", Module;
        "if", If;
        "true", BoolLiteral(true);
        "false", BoolLiteral(false);
    ]

let digits =
    Regex(@"[0-9]", RegexOptions.Compiled)

let identStartChars =
    Regex(@"[A-z_]", RegexOptions.Compiled)

let identChars =
    Regex(@"[A-z_0-9]", RegexOptions.Compiled)
