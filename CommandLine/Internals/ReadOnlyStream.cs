using System;
using System.IO;

namespace CommandLine.Internals
{
    public class ReadOnlyStream : Stream
    {
        private readonly Stream _wrappedStream;

        public ReadOnlyStream(Stream wrappedStream)
        {
            _wrappedStream = wrappedStream;
        }

        public override void Flush()
        {
            _wrappedStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _wrappedStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _wrappedStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _wrappedStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        public override bool CanRead => _wrappedStream.CanRead;

        public override bool CanSeek => _wrappedStream.CanSeek;

        public override bool CanWrite => false;

        public override long Length => _wrappedStream.Length;

        public override long Position
        {
            get { return _wrappedStream.Position; }
            set { _wrappedStream.Position = value; }
        }
    }
}