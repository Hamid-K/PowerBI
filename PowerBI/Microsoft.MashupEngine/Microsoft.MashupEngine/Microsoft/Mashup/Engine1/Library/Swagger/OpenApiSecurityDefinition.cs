using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200039B RID: 923
	internal class OpenApiSecurityDefinition : OpenApiSpecObject
	{
		// Token: 0x0600202A RID: 8234 RVA: 0x000531DB File Offset: 0x000513DB
		public OpenApiSecurityDefinition(RecordValue rawSecurityDefinition, OpenApiUserSettings userSettings)
			: base(rawSecurityDefinition, userSettings)
		{
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x00054824 File Offset: 0x00052A24
		public string Type
		{
			get
			{
				if (this.type == null)
				{
					Value value;
					this.type = (base.RawObject.TryGetValue("type", out value) ? value.AsString : string.Empty);
				}
				return this.type;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x00054868 File Offset: 0x00052A68
		public string Description
		{
			get
			{
				if (this.description == null)
				{
					Value value;
					this.description = (base.RawObject.TryGetValue("description", out value) ? value.AsString : string.Empty);
				}
				return this.description;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000548AC File Offset: 0x00052AAC
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					Value value;
					this.name = (base.RawObject.TryGetValue("name", out value) ? value.AsString : string.Empty);
				}
				return this.name;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x000548F0 File Offset: 0x00052AF0
		public string In
		{
			get
			{
				if (this.inValue == null)
				{
					Value value;
					this.inValue = (base.RawObject.TryGetValue("in", out value) ? value.AsString : string.Empty);
				}
				return this.inValue;
			}
		}

		// Token: 0x04000C3F RID: 3135
		private string type;

		// Token: 0x04000C40 RID: 3136
		private string description;

		// Token: 0x04000C41 RID: 3137
		private string name;

		// Token: 0x04000C42 RID: 3138
		private string inValue;
	}
}
