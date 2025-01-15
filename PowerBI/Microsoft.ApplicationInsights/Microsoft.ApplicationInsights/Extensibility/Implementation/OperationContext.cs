using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000073 RID: 115
	public sealed class OperationContext
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		internal OperationContext()
		{
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000FEEF File Offset: 0x0000E0EF
		public string Id
		{
			get
			{
				if (!string.IsNullOrEmpty(this.id))
				{
					return this.id;
				}
				return null;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000FF0F File Offset: 0x0000E10F
		public string ParentId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.parentId))
				{
					return this.parentId;
				}
				return null;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000FF18 File Offset: 0x0000E118
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000FF2F File Offset: 0x0000E12F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string CorrelationVector
		{
			get
			{
				if (!string.IsNullOrEmpty(this.correlationVector))
				{
					return this.correlationVector;
				}
				return null;
			}
			set
			{
				this.correlationVector = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000FF38 File Offset: 0x0000E138
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000FF4F File Offset: 0x0000E14F
		public string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(this.name))
				{
					return this.name;
				}
				return null;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000FF58 File Offset: 0x0000E158
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000FF6F File Offset: 0x0000E16F
		public string SyntheticSource
		{
			get
			{
				if (!string.IsNullOrEmpty(this.syntheticSource))
				{
					return this.syntheticSource;
				}
				return null;
			}
			set
			{
				this.syntheticSource = value;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000FF78 File Offset: 0x0000E178
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.OperationId, this.Id);
			tags.UpdateTagValue(ContextTagKeys.Keys.OperationParentId, this.ParentId);
			tags.UpdateTagValue(ContextTagKeys.Keys.OperationCorrelationVector, this.CorrelationVector);
			tags.UpdateTagValue(ContextTagKeys.Keys.OperationName, this.Name);
			tags.UpdateTagValue(ContextTagKeys.Keys.OperationSyntheticSource, this.SyntheticSource);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		internal void CopyTo(OperationContext target)
		{
			Tags.CopyTagValue(this.Id, ref target.id);
			Tags.CopyTagValue(this.ParentId, ref target.parentId);
			Tags.CopyTagValue(this.CorrelationVector, ref target.correlationVector);
			Tags.CopyTagValue(this.Name, ref target.name);
			Tags.CopyTagValue(this.SyntheticSource, ref target.syntheticSource);
		}

		// Token: 0x0400016F RID: 367
		private string id;

		// Token: 0x04000170 RID: 368
		private string parentId;

		// Token: 0x04000171 RID: 369
		private string correlationVector;

		// Token: 0x04000172 RID: 370
		private string syntheticSource;

		// Token: 0x04000173 RID: 371
		private string name;
	}
}
