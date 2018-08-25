using System;
using System.Collections.Generic;
using System.Text;

namespace LangustaBobRemake
{
    class PalinurusEngine
    {
        private Queue<Frame> _buffer;

        public void LoadFrame(Frame frame)
        {
            _buffer.Enqueue(frame);
        }

        public void DrawFrame()
        {
            if (_buffer.TryDequeue(out var frame))
            {
                ResetCursor();
                Draw(frame);
            }
        }

        private void ResetCursor()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
        }

        private void Draw(Frame frame)
        {
            foreach (var line in frame.Content)
            {
                Console.WriteLine(line);
            }
        }
    }
}
