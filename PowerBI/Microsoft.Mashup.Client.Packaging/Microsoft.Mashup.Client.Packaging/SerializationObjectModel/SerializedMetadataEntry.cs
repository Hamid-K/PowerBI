using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x02000014 RID: 20
	public class SerializedMetadataEntry
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000021A0 File Offset: 0x000003A0
		public SerializedMetadataEntry()
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F08 File Offset: 0x00001108
		public SerializedMetadataEntry(string metadataType, long value)
		{
			this.Type = metadataType;
			this.RawValue = "l" + value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F33 File Offset: 0x00001133
		public SerializedMetadataEntry(string metadataType, double value)
		{
			this.Type = metadataType;
			this.RawValue = "f" + value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F5E File Offset: 0x0000115E
		public SerializedMetadataEntry(string metadataType, string value)
		{
			this.Type = metadataType;
			this.RawValue = "s" + value;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F7E File Offset: 0x0000117E
		public SerializedMetadataEntry(string metadataType, Guid value)
		{
			this.Type = metadataType;
			this.RawValue = "c" + value.ToString("D", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FAE File Offset: 0x000011AE
		public SerializedMetadataEntry(string metadataType, DateTime value)
		{
			this.Type = metadataType;
			this.RawValue = "d" + value.ToString("o", CultureInfo.InvariantCulture);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002FDE File Offset: 0x000011DE
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002FE6 File Offset: 0x000011E6
		[XmlAttribute("Type")]
		public string Type { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002FEF File Offset: 0x000011EF
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002FF7 File Offset: 0x000011F7
		[XmlAttribute("Value")]
		public string RawValue { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003000 File Offset: 0x00001200
		[XmlIgnore]
		public virtual bool IsLong
		{
			get
			{
				return this.HasPrefix('l');
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000300A File Offset: 0x0000120A
		[XmlIgnore]
		public virtual long LongValue
		{
			get
			{
				return long.Parse(this.RawValue.Substring(1), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003022 File Offset: 0x00001222
		[XmlIgnore]
		public virtual bool IsDouble
		{
			get
			{
				return this.HasPrefix('f');
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000302C File Offset: 0x0000122C
		[XmlIgnore]
		public virtual double DoubleValue
		{
			get
			{
				return double.Parse(this.RawValue.Substring(1), CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003044 File Offset: 0x00001244
		[XmlIgnore]
		public virtual bool IsString
		{
			get
			{
				return this.HasPrefix('s');
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000304E File Offset: 0x0000124E
		[XmlIgnore]
		public virtual string StringValue
		{
			get
			{
				return this.RawValue.Substring(1);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000305C File Offset: 0x0000125C
		[XmlIgnore]
		public virtual bool IsDateTime
		{
			get
			{
				return this.HasPrefix('d');
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003066 File Offset: 0x00001266
		[XmlIgnore]
		public virtual DateTime DateTimeValue
		{
			get
			{
				return DateTime.ParseExact(this.RawValue.Substring(1), "o", CultureInfo.InvariantCulture, 16);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003085 File Offset: 0x00001285
		[XmlIgnore]
		public virtual bool IsGuid
		{
			get
			{
				return this.HasPrefix('c');
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000308F File Offset: 0x0000128F
		[XmlIgnore]
		public virtual Guid GuidValue
		{
			get
			{
				return new Guid(this.RawValue.Substring(1));
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030A2 File Offset: 0x000012A2
		private bool HasPrefix(char prefix)
		{
			return !string.IsNullOrEmpty(this.RawValue) && this.RawValue.get_Chars(0) == prefix;
		}

		// Token: 0x04000055 RID: 85
		private const char longPrefix = 'l';

		// Token: 0x04000056 RID: 86
		private const char doublePrefix = 'f';

		// Token: 0x04000057 RID: 87
		private const char stringPrefix = 's';

		// Token: 0x04000058 RID: 88
		private const char contentIDPrefix = 'c';

		// Token: 0x04000059 RID: 89
		private const char dateTimePrefix = 'd';

		// Token: 0x0400005A RID: 90
		private const string dateTimeFormat = "o";

		// Token: 0x0400005B RID: 91
		private const string guidFormat = "D";
	}
}
