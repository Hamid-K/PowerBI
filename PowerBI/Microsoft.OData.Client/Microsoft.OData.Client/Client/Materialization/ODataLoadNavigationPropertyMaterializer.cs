using System;
using System.Collections.Generic;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000F8 RID: 248
	internal class ODataLoadNavigationPropertyMaterializer : ODataReaderEntityMaterializer
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00027618 File Offset: 0x00025818
		public ODataLoadNavigationPropertyMaterializer(ODataMessageReader odataMessageReader, ODataReaderWrapper reader, IODataMaterializerContext materializerContext, EntityTrackingAdapter entityTrackingAdapter, QueryComponents queryComponents, Type expectedType, ProjectionPlan materializeEntryPlan, LoadPropertyResponseInfo responseInfo)
			: base(odataMessageReader, reader, materializerContext, entityTrackingAdapter, queryComponents, expectedType, materializeEntryPlan)
		{
			this.responseInfo = responseInfo;
			this.items = new List<object>();
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00027640 File Offset: 0x00025840
		protected override bool ReadImplementation()
		{
			if (this.iteration == 0)
			{
				while (base.ReadImplementation())
				{
					this.items.Add(this.currentValue);
				}
				ClientPropertyAnnotation property = this.responseInfo.Property;
				EntityDescriptor entityDescriptor = this.responseInfo.EntityDescriptor;
				object entity = entityDescriptor.Entity;
				MaterializerEntry materializerEntry = MaterializerEntry.CreateEntryForLoadProperty(entityDescriptor, this.Format, this.responseInfo.MergeOption != MergeOption.NoTracking);
				materializerEntry.ActualType = this.responseInfo.Model.GetClientTypeAnnotation(this.responseInfo.Model.GetOrCreateEdmType(entity.GetType()));
				if (property.IsEntityCollection)
				{
					base.EntryValueMaterializationPolicy.ApplyItemsToCollection(materializerEntry, property, this.items, (this.CurrentFeed != null) ? this.CurrentFeed.NextPageLink : null, this.MaterializeEntryPlan, this.responseInfo.IsContinuation);
				}
				else
				{
					object obj = ((this.items.Count > 0) ? this.items[0] : null);
					base.EntityTrackingAdapter.MaterializationLog.SetLink(materializerEntry, property.PropertyName, obj);
					property.SetValue(entity, obj, property.PropertyName, false);
				}
				this.ApplyLogToContext();
				this.ClearLog();
			}
			if (this.items.Count > this.iteration)
			{
				this.currentValue = this.items[this.iteration];
				this.iteration++;
				return true;
			}
			this.currentValue = null;
			return false;
		}

		// Token: 0x04000609 RID: 1545
		private LoadPropertyResponseInfo responseInfo;

		// Token: 0x0400060A RID: 1546
		private List<object> items;

		// Token: 0x0400060B RID: 1547
		private int iteration;
	}
}
