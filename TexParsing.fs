module TexParsing

open System.Drawing

type Style =
    | Normal = 0
    | Bold = 1
    | Italic = 2

[<RequireQualifiedAccess>]
type FE =
    | Word of string * Style * Color
    | Space
    | Math of string * Color  
    
type TexBlock =
    | Items of TexBlock list
    | Group of string * TexBlock list
    | Command of id:string
    | CommandVariadic of id:string * TexBlock list list
    | Text of string
    | Break
    | Empty