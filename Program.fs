module Program
open FSharp.Text.Lexing
open TexParsing
open BenchmarkDotNet.Attributes

let parse tex = 
    let lexbuf = LexBuffer<char>.FromString tex
    let res = Parser.start Lexer.tokenize lexbuf
    res

let lex tex =
     LexBuffer<char>.FromString tex
     |> List.unfold(fun buf -> 
         if buf.IsPastEndOfStream then 
             None 
         else
             let token = Lexer.tokenize buf
             Some(token, buf)
     ) 

type TestBench () =    
    let simpleTex = @"Hello World \\ \texbf"
    
    [<Benchmark>]
    member _.ParseSimple () =
        simpleTex |> parse <> TexBlock.Empty

[<EntryPoint>]
let main argv = 
 
    let sampleTex = @"Hello World \\ \texbf \textit{So it} \$ \margin{1}{2}{3}{4}"
    let lexResult = sampleTex |> lex
    printfn "Lex:\n%A" lexResult
    let parseResult = sampleTex |> parse
    printfn "Parse:\n%A" parseResult
    
    #if RELEASE
    BenchmarkDotNet.Running.BenchmarkRunner.Run<TestBench>() |> ignore
    #endif
    0