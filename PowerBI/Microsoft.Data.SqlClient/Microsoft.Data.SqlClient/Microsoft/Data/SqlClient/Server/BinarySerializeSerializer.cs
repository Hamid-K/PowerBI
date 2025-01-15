using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.SqlServer.Server;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000145 RID: 325
	internal sealed class BinarySerializeSerializer : Serializer
	{
		// Token: 0x0600197B RID: 6523 RVA: 0x0006B3E3 File Offset: 0x000695E3
		internal BinarySerializeSerializer(Type t)
			: base(t)
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0006B3EC File Offset: 0x000695EC
		public override void Serialize(Stream s, object o)
		{
			BinaryWriter binaryWriter = new BinaryWriter(s);
			((IBinarySerialize)o).Write(binaryWriter);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0006B40C File Offset: 0x0006960C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Deserialize(Stream s)
		{
			object obj = Activator.CreateInstance(this._type);
			BinaryReader binaryReader = new BinaryReader(s);
			((IBinarySerialize)obj).Read(binaryReader);
			return obj;
		}
	}
}
