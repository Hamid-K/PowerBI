using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A0 RID: 416
	public class MapBucket : ReportObject
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00022886 File Offset: 0x00020A86
		public MapBucket()
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002288E File Offset: 0x00020A8E
		internal MapBucket(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00022897 File Offset: 0x00020A97
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x000228A5 File Offset: 0x00020AA5
		public ReportExpression StartValue
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000228B9 File Offset: 0x00020AB9
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x000228C7 File Offset: 0x00020AC7
		public ReportExpression EndValue
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000228DB File Offset: 0x00020ADB
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x020003CC RID: 972
		internal class Definition : DefinitionStore<MapBucket, MapBucket.Definition.Properties>
		{
			// Token: 0x06001870 RID: 6256 RVA: 0x0003B701 File Offset: 0x00039901
			private Definition()
			{
			}

			// Token: 0x020004E4 RID: 1252
			internal enum Properties
			{
				// Token: 0x04000FFD RID: 4093
				StartValue,
				// Token: 0x04000FFE RID: 4094
				EndValue,
				// Token: 0x04000FFF RID: 4095
				PropertyCount
			}
		}
	}
}
