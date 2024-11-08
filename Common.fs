module Common

type Bytecode =
    // Load int8 literal
    | LDI08

    // Load int16 literal
    | LDI16

    // Load int32 literal
    | LDI32

    // Load int64 literal
    | LDI64

    // Load float32 literal
    | LDF32

    // Load float64 literal
    | LDF64

    // Load boolean literal
    | LDBLN

    // Load (local) variable
    | LDVAR

    // Set (local) variable
    | STVAR

    // Load param
    | LDPAR

    // Call method returning value
    | CALLV

    // Call method returning unit
    | CALLU

    // Return value
    | RETNV

    // Return unit
    | RETNU

type Ident = string

type Function = {
    name: string
    arity: byte
    body: list<Bytecode>
}
