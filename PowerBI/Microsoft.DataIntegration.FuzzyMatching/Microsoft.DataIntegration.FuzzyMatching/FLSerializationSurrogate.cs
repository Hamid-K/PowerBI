using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200004B RID: 75
	internal class FLSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IRawSerializable rawSerializable = obj as IRawSerializable;
			if (!this.m_distinctObjects.Contains(obj))
			{
				this.m_distinctObjects.Add(obj);
				this.m_rawSerializableObjects.Add(rawSerializable);
				rawSerializable.EnableRawSerialization = true;
				rawSerializable.RawSerializationID = this.m_rawSerializableObjects.Count;
			}
			rawSerializable.GetObjectData(info, context);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			Type type = obj.GetType();
			ConstructorInfo constructor = type.GetConstructor(52, null, 3, FLSerializationSurrogate.SIConstructorTypes, null);
			if (constructor == null)
			{
				throw new SerializationException(string.Format("Serialization constructor not found for type {0}", type.FullName));
			}
			obj = constructor.Invoke(new object[] { info, context });
			IRawSerializable rawSerializable = obj as IRawSerializable;
			this.m_rawSerializableObjects.Add(rawSerializable);
			return obj;
		}

		// Token: 0x040000E1 RID: 225
		private static readonly Type[] SIConstructorTypes = new Type[]
		{
			typeof(SerializationInfo),
			typeof(StreamingContext)
		};

		// Token: 0x040000E2 RID: 226
		private HashSet<object> m_distinctObjects = new HashSet<object>();

		// Token: 0x040000E3 RID: 227
		public List<IRawSerializable> m_rawSerializableObjects = new List<IRawSerializable>();
	}
}
