using System;
using System.Reflection;

namespace Microsoft.OData.Client
{
	// Token: 0x02000070 RID: 112
	internal sealed class BinaryTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000E820 File Offset: 0x0000CA20
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000E827 File Offset: 0x0000CA27
		internal static Type BinaryType { get; set; }

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E82F File Offset: 0x0000CA2F
		internal override object Parse(string text)
		{
			return Activator.CreateInstance(BinaryTypeConverter.BinaryType, new object[] { Convert.FromBase64String(text) });
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E84A File Offset: 0x0000CA4A
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000E852 File Offset: 0x0000CA52
		internal byte[] ToArray(object instance)
		{
			if (this.convertToByteArrayMethodInfo == null)
			{
				this.convertToByteArrayMethodInfo = instance.GetType().GetMethod("ToArray", BindingFlags.Instance | BindingFlags.Public);
			}
			return (byte[])this.convertToByteArrayMethodInfo.Invoke(instance, null);
		}

		// Token: 0x0400012D RID: 301
		private MethodInfo convertToByteArrayMethodInfo;
	}
}
