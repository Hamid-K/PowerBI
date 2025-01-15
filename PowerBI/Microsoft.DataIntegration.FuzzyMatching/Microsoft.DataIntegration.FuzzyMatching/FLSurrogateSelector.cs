using System;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200004C RID: 76
	internal class FLSurrogateSelector : SurrogateSelector, ISurrogateSelector
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000DCEE File Offset: 0x0000BEEE
		ISerializationSurrogate ISurrogateSelector.GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (!type.IsPrimitive && typeof(IRawSerializable).IsAssignableFrom(type))
			{
				selector = this;
				return this.m_serializationSurrogate;
			}
			return base.GetSurrogate(type, context, ref selector);
		}

		// Token: 0x040000E4 RID: 228
		internal FLSerializationSurrogate m_serializationSurrogate = new FLSerializationSurrogate();
	}
}
