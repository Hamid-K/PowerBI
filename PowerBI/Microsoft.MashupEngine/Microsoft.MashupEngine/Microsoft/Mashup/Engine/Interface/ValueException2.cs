using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public abstract class ValueException2 : RuntimeException
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00002BB1 File Offset: 0x00000DB1
		protected ValueException2()
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00004DA2 File Offset: 0x00002FA2
		protected ValueException2(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00002BC2 File Offset: 0x00000DC2
		protected ValueException2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600051C RID: 1308
		public abstract IRecordValue Value2 { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00007B80 File Offset: 0x00005D80
		public string ReasonString
		{
			get
			{
				IValue value;
				if (!this.Value2.TryGetValue("Reason", out value) || !value.IsText)
				{
					return null;
				}
				return value.AsString;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public bool ReasonIsPii
		{
			get
			{
				IValue value;
				return !this.Value2.TryGetValue("Reason", out value) || !value.IsText || ValueException2.IsPii(value);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public string MessageFormatString
		{
			get
			{
				IValue value;
				if (!this.Value2.TryGetValue("Message.Format", out value) || !value.IsText)
				{
					return null;
				}
				return value.AsString;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00007C1C File Offset: 0x00005E1C
		public bool MessageFormatIsPii
		{
			get
			{
				IValue value;
				return !this.Value2.TryGetValue("Message.Format", out value) || !value.IsText || ValueException2.IsPii(value);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00007C50 File Offset: 0x00005E50
		public string MessageString
		{
			get
			{
				IValue value;
				if (!this.Value2.TryGetValue("Message", out value) || !value.IsText)
				{
					return null;
				}
				return value.AsString;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00007C84 File Offset: 0x00005E84
		public bool MessageIsPii
		{
			get
			{
				IValue value;
				return !this.Value2.TryGetValue("Message", out value) || !value.IsText || ValueException2.IsPii(value);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public IValue Detail
		{
			get
			{
				IValue value;
				if (!this.Value2.TryGetValue("Detail", out value))
				{
					return null;
				}
				return value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00007CDC File Offset: 0x00005EDC
		public bool DetailIsPii
		{
			get
			{
				IValue value;
				return !this.Value2.TryGetValue("Detail", out value) || ValueException2.IsPii(value);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00007D08 File Offset: 0x00005F08
		public IListValue MessageParameters
		{
			get
			{
				IValue value;
				if (!this.Value2.TryGetValue("Message.Parameters", out value) || !value.IsList)
				{
					return null;
				}
				return value.AsList;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00007D3C File Offset: 0x00005F3C
		public static bool IsPii(IValue value)
		{
			IValue value2;
			return !value.TryGetMetaField("Is.Pii", out value2) || !value2.IsLogical || !value2.AsBoolean.Equals(false);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00007D74 File Offset: 0x00005F74
		public static IValue MarkPii(IEngine engine, IValue value, bool isPii)
		{
			return value.NewMeta(value.MetaValue.Concatenate(engine.Record(engine.Keys(new string[] { "Is.Pii" }), new IValue[] { engine.Logical(isPii) })).AsRecord);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00007DC1 File Offset: 0x00005FC1
		public static string ConvertToCSharpFormatSpecifier(string messageFormat)
		{
			return ValueException2.MashupFormatSpecifierRegex.Replace(ValueException2.NonMashupFormatSpecifierRegex.Replace(messageFormat, "$1{$2}"), "$1$3");
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00007DE2 File Offset: 0x00005FE2
		public static string ConvertToMashupFormatSpecifier(string messageFormat)
		{
			return ValueException2.NonCSharpFormatSpecifierRegex.Replace(ValueException2.CSharpFormatSpecifierRegex.Replace(messageFormat, "$1#$2"), "$1$3");
		}

		// Token: 0x04000325 RID: 805
		public const string ReasonKey = "Reason";

		// Token: 0x04000326 RID: 806
		public const string MessageKey = "Message";

		// Token: 0x04000327 RID: 807
		public const string DetailKey = "Detail";

		// Token: 0x04000328 RID: 808
		public const string MessageFormatKey = "Message.Format";

		// Token: 0x04000329 RID: 809
		public const string MessageParametersKey = "Message.Parameters";

		// Token: 0x0400032A RID: 810
		public const string MessageParametersLengthKey = "MessageParametersLength";

		// Token: 0x0400032B RID: 811
		public const string PiiFlagsKey = "PiiFlags";

		// Token: 0x0400032C RID: 812
		public const string IsPiiString = "Is.Pii";

		// Token: 0x0400032D RID: 813
		public const string DataKey = "Data";

		// Token: 0x0400032E RID: 814
		public static readonly Regex MashupFormatSpecifierRegex = new Regex("(^|[^{])(#)({[0-9]+})", RegexOptions.Compiled);

		// Token: 0x0400032F RID: 815
		public static readonly Regex NonMashupFormatSpecifierRegex = new Regex("([^#])({[0-9]+})", RegexOptions.Compiled);

		// Token: 0x04000330 RID: 816
		public static readonly Regex CSharpFormatSpecifierRegex = new Regex("(^|[^{])({[0-9]+})", RegexOptions.Compiled);

		// Token: 0x04000331 RID: 817
		public static readonly Regex NonCSharpFormatSpecifierRegex = new Regex("(^|[^{])({)({[0-9]+})(})", RegexOptions.Compiled);

		// Token: 0x04000332 RID: 818
		public const string MashupFormatSpecifierSubstitution = "$1$3";

		// Token: 0x04000333 RID: 819
		public const string NonMashupFormatSpecifierSubstitution = "$1{$2}";

		// Token: 0x04000334 RID: 820
		public const string CSharpFormatSpecifierSubstitution = "$1#$2";

		// Token: 0x04000335 RID: 821
		public const string NonCSharpFormatSpecifierSubstitution = "$1$3";
	}
}
