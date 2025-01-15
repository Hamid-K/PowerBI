using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	public class MashupValueException : MashupException, IProduceRuntimeException
	{
		// Token: 0x06000364 RID: 868 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		public MashupValueException(string message, string reason)
			: this(message, reason, true, true, true, null, true, null, null)
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public MashupValueException(string message, string reason, bool reasonIsPii, bool messageIsPii, bool detailIsPii, string messageFormat = null, bool messageFormatIsPii = true, string[] messageParameters = null, bool[] messageParametersIsPii = null)
			: base(message)
		{
			this.Reason = reason;
			this.Data[MashupValueException.ReasonKey] = reason;
			this.ReasonIsPii = reasonIsPii;
			this.MessageIsPii = messageIsPii;
			this.DetailIsPii = detailIsPii;
			this.MessageFormat = messageFormat;
			this.MessageFormatIsPii = messageFormatIsPii;
			this.MessageParameters = messageParameters;
			this.MessageParametersIsPii = messageParametersIsPii;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000D353 File Offset: 0x0000B553
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000D35B File Offset: 0x0000B55B
		public string Reason { get; private set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000D364 File Offset: 0x0000B564
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000D36C File Offset: 0x0000B56C
		public bool ReasonIsPii { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000D375 File Offset: 0x0000B575
		// (set) Token: 0x0600036B RID: 875 RVA: 0x0000D37D File Offset: 0x0000B57D
		public bool MessageIsPii { get; private set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000D386 File Offset: 0x0000B586
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000D38E File Offset: 0x0000B58E
		public bool DetailIsPii { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000D397 File Offset: 0x0000B597
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000D39F File Offset: 0x0000B59F
		public string MessageFormat { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		public bool MessageFormatIsPii { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000D3B9 File Offset: 0x0000B5B9
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		public string[] MessageParameters { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000D3CA File Offset: 0x0000B5CA
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000D3D2 File Offset: 0x0000B5D2
		public bool[] MessageParametersIsPii { get; private set; }

		// Token: 0x06000376 RID: 886 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(MashupValueException.ReasonKey, this.Reason);
			info.AddValue(MashupValueException.ReasonIsPiiKey, this.ReasonIsPii);
			info.AddValue(MashupValueException.MessageIsPiiKey, this.MessageIsPii);
			info.AddValue(MashupValueException.DetailIsPiiKey, this.DetailIsPii);
			info.AddValue(MashupValueException.MessageFormatKey, this.MessageFormat);
			info.AddValue(MashupValueException.MessageFormatIsPiiKey, this.MessageFormatIsPii);
			info.AddValue(MashupValueException.MessageParametersKey, this.MessageParameters);
			info.AddValue(MashupValueException.MessageParametersIsPiiKey, this.MessageParametersIsPii);
			base.GetObjectData(info, context);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D47C File Offset: 0x0000B67C
		protected MashupValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Reason = info.GetString(MashupValueException.ReasonKey);
			this.ReasonIsPii = (bool)info.GetValue(MashupValueException.ReasonIsPiiKey, typeof(bool));
			this.MessageIsPii = (bool)info.GetValue(MashupValueException.MessageIsPiiKey, typeof(bool));
			this.DetailIsPii = (bool)info.GetValue(MashupValueException.DetailIsPiiKey, typeof(bool));
			this.MessageFormat = info.GetString(MashupValueException.MessageFormatKey);
			this.MessageFormatIsPii = (bool)info.GetValue(MashupValueException.MessageFormatIsPiiKey, typeof(bool));
			this.MessageParameters = (string[])info.GetValue(MashupValueException.MessageParametersKey, typeof(string[]));
			this.MessageParametersIsPii = (bool[])info.GetValue(MashupValueException.MessageParametersIsPiiKey, typeof(bool[]));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000D573 File Offset: 0x0000B773
		public string GetPiiSafeReason(Func<string, string> scrubber)
		{
			if (!this.ReasonIsPii)
			{
				return this.Reason;
			}
			return scrubber(this.Reason);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000D590 File Offset: 0x0000B790
		public string GetPiiSafeMessage(Func<string, string> scrubber)
		{
			if (this.MessageIsPii && this.MessageFormat != null && this.MessageParameters != null && !this.MessageFormatIsPii)
			{
				string[] array = new string[this.MessageParameters.Length];
				for (int i = 0; i < this.MessageParameters.Length; i++)
				{
					array[i] = (this.MessageParametersIsPii[i] ? scrubber(this.MessageParameters[i]) : this.MessageParameters[i]);
				}
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				string messageFormat = this.MessageFormat;
				object[] array2 = array;
				return string.Format(invariantCulture, messageFormat, array2);
			}
			if (!this.MessageIsPii)
			{
				return this.Message;
			}
			return scrubber(this.Message);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000D632 File Offset: 0x0000B832
		internal static string ValueDataKey(string key)
		{
			return MashupException.DataKey(new string[] { "ValueError", key });
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000D64C File Offset: 0x0000B84C
		internal static string FromValueDataKey(string dataKey)
		{
			string[] array = MashupException.FromDataKey(dataKey);
			if (array.Length != 0 && array[0].Equals("ValueError"))
			{
				return string.Join(".", array.Skip(1).ToArray<string>());
			}
			return dataKey;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000D68C File Offset: 0x0000B88C
		RuntimeException IProduceRuntimeException.GetRuntimeException()
		{
			IEngine engine = MashupEngines.Version1;
			Func<string, IValue> func = delegate(string s)
			{
				if (s != null)
				{
					return engine.Text(s);
				}
				return engine.Null;
			};
			IValue value = engine.Null;
			if (this.MessageParameters != null)
			{
				IValue[] array = new IValue[this.MessageParameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ValueException2.MarkPii(engine, func(this.MessageParameters[i]), this.MessageParametersIsPii[i]);
				}
				value = engine.List(array);
			}
			return engine.Exception(engine.ExceptionRecord(ValueException2.MarkPii(engine, engine.Text(this.Reason ?? "<unknown>"), this.ReasonIsPii).AsText, ValueException2.MarkPii(engine, func(this.Message), this.MessageIsPii), engine.Null, ValueException2.MarkPii(engine, func(this.MessageFormat), this.MessageFormatIsPii), value));
		}

		// Token: 0x040001AD RID: 429
		internal const string ValueErrorPart = "ValueError";

		// Token: 0x040001AE RID: 430
		internal const string ReasonPart = "Reason";

		// Token: 0x040001AF RID: 431
		internal const string ReasonIsPiiPart = "ReasonIsPii";

		// Token: 0x040001B0 RID: 432
		internal const string MessageIsPiiPart = "MessageIsPii";

		// Token: 0x040001B1 RID: 433
		internal const string DetailIsPiiPart = "DetailIsPii";

		// Token: 0x040001B2 RID: 434
		internal const string MessageFormatPart = "MessageFormat";

		// Token: 0x040001B3 RID: 435
		internal const string MessageFormatIsPiiPart = "MessageFormatIsPii";

		// Token: 0x040001B4 RID: 436
		internal const string MessageParametersPart = "MessageParameters";

		// Token: 0x040001B5 RID: 437
		internal const string MessageParametersIsPiiPart = "MessageParametersIsPii";

		// Token: 0x040001B6 RID: 438
		internal static string ReasonKey = MashupValueException.ValueDataKey("Reason");

		// Token: 0x040001B7 RID: 439
		internal static string ReasonIsPiiKey = MashupValueException.ValueDataKey("ReasonIsPii");

		// Token: 0x040001B8 RID: 440
		internal static string MessageIsPiiKey = MashupValueException.ValueDataKey("MessageIsPii");

		// Token: 0x040001B9 RID: 441
		internal static string DetailIsPiiKey = MashupValueException.ValueDataKey("DetailIsPii");

		// Token: 0x040001BA RID: 442
		internal static string MessageFormatKey = MashupValueException.ValueDataKey("MessageFormat");

		// Token: 0x040001BB RID: 443
		internal static string MessageFormatIsPiiKey = MashupValueException.ValueDataKey("MessageFormatIsPii");

		// Token: 0x040001BC RID: 444
		internal static string MessageParametersKey = MashupValueException.ValueDataKey("MessageParameters");

		// Token: 0x040001BD RID: 445
		internal static string MessageParametersIsPiiKey = MashupValueException.ValueDataKey("MessageParametersIsPii");

		// Token: 0x040001BE RID: 446
		internal static string DataSourceKindKey = MashupValueException.ValueDataKey("DataSourceKind");

		// Token: 0x040001BF RID: 447
		internal static string DataSourcePathKey = MashupValueException.ValueDataKey("DataSourcePath");

		// Token: 0x040001C0 RID: 448
		internal static string ClientLibraryNameKey = MashupValueException.ValueDataKey("ClientLibraryName");

		// Token: 0x040001C1 RID: 449
		internal static string DownloadLinkKey = MashupValueException.ValueDataKey("DownloadLink");
	}
}
