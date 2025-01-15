using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E49 RID: 3657
	internal class EventsAndSplitsHelper
	{
		// Token: 0x06006256 RID: 25174 RVA: 0x00151B0B File Offset: 0x0014FD0B
		public EventsAndSplitsHelper(CdpaService service, ICube cube)
		{
			this.cube = cube;
			this.visitor = new EventsAndSplitsHelper.EventsAndSplitsVisitor(this);
			this.tenant = new CdpaTenant
			{
				TargetDocumentId = service.Tenant
			};
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x00151B40 File Offset: 0x0014FD40
		public void AddEventsAndSplits(CdpaDimensionAttribute attribute, HashSet<QualifiedName> dimensions, HashSet<CdpaEvent> events, HashSet<CdpaMetricSplit> splits)
		{
			HashSet<QualifiedName> hashSet = this.dimensions;
			HashSet<CdpaEvent> hashSet2 = this.events;
			HashSet<CdpaMetricSplit> hashSet3 = this.splits;
			this.dimensions = dimensions;
			this.events = events;
			this.splits = splits;
			try
			{
				this.AddEventsAndSplits(attribute);
			}
			finally
			{
				this.dimensions = hashSet;
				this.events = hashSet2;
				this.splits = hashSet3;
			}
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x00151BA8 File Offset: 0x0014FDA8
		private void AddEventsAndSplits(CdpaDimensionAttribute attribute)
		{
			CdpaHierarchyLevel cdpaHierarchyLevel = attribute as CdpaHierarchyLevel;
			if (cdpaHierarchyLevel != null)
			{
				this.AddEventsAndSplits(cdpaHierarchyLevel.Attribute);
				return;
			}
			CdpaSignalDimensionAttribute cdpaSignalDimensionAttribute = attribute as CdpaSignalDimensionAttribute;
			if (cdpaSignalDimensionAttribute != null)
			{
				this.splits.Add(new AllWithLimitsCdpaMetricSplit
				{
					PropertyName = attribute.PropertyName
				});
				this.events.Add(new CdpaEvent
				{
					Tenant = this.tenant,
					EventName = cdpaSignalDimensionAttribute.Dimension.SignalName
				});
				return;
			}
			CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute = attribute as CdpaRelatedDimensionAttribute;
			if (cdpaRelatedDimensionAttribute != null)
			{
				this.dimensions.Add(cdpaRelatedDimensionAttribute.Dimension.QualifiedName);
				foreach (CdpaDimensionAttribute cdpaDimensionAttribute in cdpaRelatedDimensionAttribute.RelatedAttributes)
				{
					this.AddEventsAndSplits(cdpaDimensionAttribute);
				}
				return;
			}
			CdpaProjectedDimensionAttribute cdpaProjectedDimensionAttribute = attribute as CdpaProjectedDimensionAttribute;
			if (cdpaProjectedDimensionAttribute != null)
			{
				this.visitor.AddEventsAndSplits(cdpaProjectedDimensionAttribute.Projection);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0400359C RID: 13724
		private readonly ICube cube;

		// Token: 0x0400359D RID: 13725
		private readonly EventsAndSplitsHelper.EventsAndSplitsVisitor visitor;

		// Token: 0x0400359E RID: 13726
		private readonly CdpaTenant tenant;

		// Token: 0x0400359F RID: 13727
		private HashSet<QualifiedName> dimensions;

		// Token: 0x040035A0 RID: 13728
		private HashSet<CdpaEvent> events;

		// Token: 0x040035A1 RID: 13729
		private HashSet<CdpaMetricSplit> splits;

		// Token: 0x02000E4A RID: 3658
		private class EventsAndSplitsVisitor : CubeExpressionVisitor
		{
			// Token: 0x06006259 RID: 25177 RVA: 0x00151CB0 File Offset: 0x0014FEB0
			public EventsAndSplitsVisitor(EventsAndSplitsHelper helper)
			{
				this.helper = helper;
			}

			// Token: 0x0600625A RID: 25178 RVA: 0x00151CBF File Offset: 0x0014FEBF
			public void AddEventsAndSplits(CubeExpression expression)
			{
				this.Visit(expression);
			}

			// Token: 0x0600625B RID: 25179 RVA: 0x00151CCC File Offset: 0x0014FECC
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				if (this.helper.cube.TryGetObject(identifier, out cubeObject))
				{
					ScopePath scopePath;
					CdpaDimensionAttribute cdpaDimensionAttribute = cubeObject.GetUnscoped(out scopePath) as CdpaDimensionAttribute;
					if (cdpaDimensionAttribute != null)
					{
						this.helper.AddEventsAndSplits(cdpaDimensionAttribute);
						return base.VisitIdentifier(identifier);
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x040035A2 RID: 13730
			private readonly EventsAndSplitsHelper helper;
		}
	}
}
