namespace PalinurusEngine

open System

module Drawing =
    let private drawColouredChar (colouredChar : ColouredChar) =
        let originalForegroundColour = Console.ForegroundColor
        let originalBackgroundColour = Console.BackgroundColor

        Console.BackgroundColor <- colouredChar.BackgroundColour
        Console.ForegroundColor <- colouredChar.Colour
        Console.Write colouredChar.Character

        Console.ForegroundColor <- originalForegroundColour
        Console.BackgroundColor <- originalBackgroundColour

    let private drawThing (thing : ThingRepresentation) (position : Position) = 
        Console.SetCursorPosition(position.LeftOffset, position.TopOffset)
        thing.Top
        |> Seq.iter drawColouredChar
        
        Console.SetCursorPosition(position.LeftOffset, (position.TopOffset+1))
        thing.Middle
        |> Seq.iter drawColouredChar
        
        Console.SetCursorPosition(position.LeftOffset, (position.TopOffset+2))
        thing.Bottom
        |> Seq.iter drawColouredChar

    let private drawingAgent =
        MailboxProcessor.Start(fun inbox -> 
            let rec messageLoop() = async {
                let! (thing, position) = inbox.Receive()
                drawThing thing position
                return! messageLoop ()
            }
            messageLoop ()
        )

    let postDrawRequest(thing, position) = drawingAgent.Post(thing, position)
