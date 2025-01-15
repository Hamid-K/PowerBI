using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000945 RID: 2373
	public class HisEncoding
	{
		// Token: 0x0600439A RID: 17306 RVA: 0x000E43E4 File Offset: 0x000E25E4
		public static string StringFromBytes(byte[] value, int ccsid)
		{
			string text;
			try
			{
				object obj = HisEncoding.GetEncodingInfo.Invoke(null, new object[] { ccsid });
				text = HisEncoding.GetStringInfo.Invoke(obj, new object[] { value }) as string;
			}
			catch (TargetInvocationException ex)
			{
				ArgumentException ex2 = ex.InnerException as ArgumentException;
				if (ex2 != null && ex2.ParamName == "codepage")
				{
					throw new UnsupportedCcsidException(string.Format(CultureInfo.InvariantCulture, "Unsupported CCSID value: {0}", ccsid), ex2);
				}
				throw;
			}
			return text;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000E4478 File Offset: 0x000E2678
		public static byte[] BytesFromString(string value, int codedCharSetId)
		{
			byte[] array;
			try
			{
				object obj = HisEncoding.GetEncodingInfo.Invoke(null, new object[] { codedCharSetId });
				array = HisEncoding.GetBytesInfo.Invoke(obj, new object[] { value }) as byte[];
			}
			catch (TargetInvocationException ex)
			{
				ArgumentException ex2 = ex.InnerException as ArgumentException;
				if (ex2 != null && ex2.ParamName == "codepage")
				{
					throw new UnsupportedCcsidException(Strings.Unsupported_CodedCharSetId(codedCharSetId), ex2);
				}
				throw;
			}
			return array;
		}

		// Token: 0x040023A6 RID: 9126
		public const int IBM_UTF8 = 1208;

		// Token: 0x040023A7 RID: 9127
		private static readonly Type HisEncodingType = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.Nls.HisEncoding");

		// Token: 0x040023A8 RID: 9128
		private static readonly Type HisForwardEncodingType = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.Nls.HisForwardEncoding");

		// Token: 0x040023A9 RID: 9129
		private static readonly MethodInfo GetEncodingInfo = HisEncoding.HisEncodingType.GetMethod("GetEncoding", new Type[] { typeof(int) });

		// Token: 0x040023AA RID: 9130
		private static readonly MethodInfo GetStringInfo = HisEncoding.HisForwardEncodingType.GetMethod("GetString", new Type[] { typeof(byte[]) });

		// Token: 0x040023AB RID: 9131
		private static readonly MethodInfo GetBytesInfo = HisEncoding.HisForwardEncodingType.GetMethod("GetBytes", new Type[] { typeof(string) });
	}
}
