using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class MashupException : DbException
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000ADA1 File Offset: 0x00008FA1
		public MashupException()
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		public MashupException(string message)
			: base(message)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000ADB2 File Offset: 0x00008FB2
		public MashupException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000ADBC File Offset: 0x00008FBC
		protected MashupException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		internal static string DataKey(params string[] keyParts)
		{
			StringBuilder stringBuilder = new StringBuilder("Microsoft.Data.Mashup");
			foreach (string text in keyParts)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AE0C File Offset: 0x0000900C
		internal static string[] FromDataKey(string dataKey)
		{
			if (dataKey.StartsWith("Microsoft.Data.Mashup", StringComparison.Ordinal))
			{
				int num = Math.Min("Microsoft.Data.Mashup".Length + 1, dataKey.Length);
				return dataKey.Substring(num).Split(new char[] { '.' });
			}
			return new string[] { dataKey };
		}

		// Token: 0x04000158 RID: 344
		private const string DataKeyPrefix = "Microsoft.Data.Mashup";
	}
}
