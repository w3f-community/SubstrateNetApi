﻿using System;
using Newtonsoft.Json;

namespace SubstrateNetApi.Model.Types
{
    public abstract class BaseType<T> : IType
    {
        [JsonIgnore] public byte[] Bytes { get; internal set; }

        public T Value { get; internal set; }
        public abstract string Name();

        public abstract int Size();

        public abstract byte[] Encode();

        public void Decode(byte[] byteArray, ref int p)
        {
            var memory = byteArray.AsMemory();
            var result = memory.Span.Slice(p, Size()).ToArray();
            p += Size();
            Create(result);
        }

        public virtual void Create(string str)
        {
            Create(Utils.HexToByteArray(str));
        }

        public virtual void CreateFromJson(string str)
        {
            Create(Utils.HexToByteArray(str));
        }

        public abstract void Create(byte[] byteArray);

        public IType New()
        {
            return this;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Value);
        }
    }
}