using System;
using System.IO;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000144 RID: 324
	internal sealed class NormalizedSerializer : Serializer
	{
		// Token: 0x06001978 RID: 6520 RVA: 0x0006B3A3 File Offset: 0x000695A3
		internal NormalizedSerializer(Type t)
			: base(t)
		{
			SerializationHelperSql9.GetUdtAttribute(t);
			this._normalizer = new BinaryOrderedUdtNormalizer(t, true);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0006B3C0 File Offset: 0x000695C0
		public override void Serialize(Stream s, object o)
		{
			this._normalizer.NormalizeTopObject(o, s);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0006B3CF File Offset: 0x000695CF
		public override object Deserialize(Stream s)
		{
			return this._normalizer.DeNormalizeTopObject(this._type, s);
		}

		// Token: 0x040009CF RID: 2511
		private readonly BinaryOrderedUdtNormalizer _normalizer;
	}
}
