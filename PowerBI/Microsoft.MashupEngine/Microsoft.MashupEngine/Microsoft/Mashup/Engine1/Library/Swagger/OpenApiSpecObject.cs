using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200039C RID: 924
	internal abstract class OpenApiSpecObject
	{
		// Token: 0x0600202F RID: 8239 RVA: 0x00054932 File Offset: 0x00052B32
		protected OpenApiSpecObject(RecordValue rawObject, OpenApiUserSettings userSettings)
		{
			this.rawObject = rawObject;
			this.userSettings = userSettings;
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x00054948 File Offset: 0x00052B48
		public OpenApiUserSettings UserSettings
		{
			get
			{
				return this.userSettings;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00054950 File Offset: 0x00052B50
		public Value VendorExtensions
		{
			get
			{
				if (this.vendorExtensions == null)
				{
					this.vendorExtensions = this.BuildVendorExtensions();
				}
				return this.vendorExtensions;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0005496C File Offset: 0x00052B6C
		protected RecordValue RawObject
		{
			get
			{
				return this.rawObject;
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00054974 File Offset: 0x00052B74
		public RecordValue GetMetadata()
		{
			if (this.meta == null)
			{
				List<RecordKeyDefinition> metaDataRecords = this.GetMetaDataRecords();
				RecordBuilder recordBuilder = new RecordBuilder(metaDataRecords.Count);
				recordBuilder.Add(metaDataRecords);
				this.meta = recordBuilder.ToRecord();
			}
			return this.meta;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000549B8 File Offset: 0x00052BB8
		protected virtual List<RecordKeyDefinition> GetMetaDataRecords()
		{
			List<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			if (this.VendorExtensions != Value.Null && this.UserSettings.IncludeExtensions)
			{
				list.Add(new RecordKeyDefinition("VendorExtensions", this.VendorExtensions, TypeValue.Any));
			}
			return list;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00054A04 File Offset: 0x00052C04
		private Value BuildVendorExtensions()
		{
			RecordValue recordValue = this.RawObject;
			List<string> list = recordValue.Keys.Where((string s) => s.StartsWith("x-", StringComparison.Ordinal)).ToList<string>();
			if (list.Count == 0)
			{
				return Value.Null;
			}
			RecordBuilder recordBuilder = new RecordBuilder(list.Count);
			foreach (string text in list)
			{
				Value value = recordValue[text];
				recordBuilder.Add(text, value, value.Type);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x04000C43 RID: 3139
		private readonly RecordValue rawObject;

		// Token: 0x04000C44 RID: 3140
		private readonly OpenApiUserSettings userSettings;

		// Token: 0x04000C45 RID: 3141
		private Value vendorExtensions;

		// Token: 0x04000C46 RID: 3142
		private RecordValue meta;
	}
}
