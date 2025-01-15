using System;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200010E RID: 270
	public class PropertyChangeEntry
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x0007DE87 File Offset: 0x0007C087
		internal PropertyChangeEntry()
		{
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0007DE8F File Offset: 0x0007C08F
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x0007DE97 File Offset: 0x0007C097
		public MetadataObject Object { get; internal set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0007DEA0 File Offset: 0x0007C0A0
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x0007DEA8 File Offset: 0x0007C0A8
		public string PropertyName { get; internal set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0007DEB1 File Offset: 0x0007C0B1
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0007DEB9 File Offset: 0x0007C0B9
		public Type PropertyType { get; internal set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0007DEC2 File Offset: 0x0007C0C2
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0007DECA File Offset: 0x0007C0CA
		public object OriginalValue { get; internal set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0007DED3 File Offset: 0x0007C0D3
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0007DEDB File Offset: 0x0007C0DB
		public object NewValue { get; internal set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0007DEE4 File Offset: 0x0007C0E4
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0007DEEC File Offset: 0x0007C0EC
		internal PropertyFlags Flags { get; set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0007DEF5 File Offset: 0x0007C0F5
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0007DEFD File Offset: 0x0007C0FD
		internal ObjectPath OriginalPath { get; set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0007DF06 File Offset: 0x0007C106
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0007DF0E File Offset: 0x0007C10E
		internal ObjectPath NewPath { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0007DF17 File Offset: 0x0007C117
		internal bool IsDDLProperty
		{
			get
			{
				return (this.Flags & PropertyFlags.Ddl) == PropertyFlags.Ddl;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0007DF24 File Offset: 0x0007C124
		internal bool IsUserProperty
		{
			get
			{
				return (this.Flags & PropertyFlags.User) == PropertyFlags.User;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0007DF31 File Offset: 0x0007C131
		internal bool IsReadOnlyProperty
		{
			get
			{
				return (this.Flags & PropertyFlags.ReadOnly) == PropertyFlags.ReadOnly;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0007DF3E File Offset: 0x0007C13E
		internal bool IsJson
		{
			get
			{
				return (this.Flags & PropertyFlags.Json) == PropertyFlags.Json;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0007DF4C File Offset: 0x0007C14C
		internal void Validate()
		{
			if (this.PropertyType == typeof(MetadataObject) || this.PropertyType.IsSubclassOf(typeof(MetadataObject)))
			{
				ValidationUtil.ValidateLink(this.Object, this.PropertyName, this.NewValue as MetadataObject, this.NewPath, null, true);
			}
		}
	}
}
