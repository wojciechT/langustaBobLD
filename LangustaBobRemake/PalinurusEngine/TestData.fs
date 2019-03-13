namespace PalinurusEngine

open System

module TestData =
    let top1 = {Character = ' '; Colour = ConsoleColor.White; BackgroundColour = ConsoleColor.DarkBlue}
    let top2 = {top1 with Character = '|'}
    let top3 = {top1 with Character = '-'}
    let top4 = {top1 with Character = '-'}
    let top5 = {top1 with Character = ' '}
    let middle1 = {top1 with Character = 'o'}
    let middle2 = {top1 with Character = '='}
    let middle3 = {top1 with Character = 'D'}
    let middle4 = {top1 with Character = '|'}
    let middle5 = {top1 with Character = '='}
    let bottom1 = top1
    let bottom2 = {top1 with Character = '|'}
    let bottom3 = {top1 with Character = '-'}
    let bottom4 = {top1 with Character = '-'}
    let bottom5 = {top1 with Character = ' '}
    
    let pirateRepresentation = 
        {
            Top = [|top1; top2; top3; top4; top5;|];
            Middle = [|middle1; middle2; middle3; middle4; middle5;|];
            Bottom = [|bottom1; bottom2; bottom3; bottom4; bottom5;|];
        }