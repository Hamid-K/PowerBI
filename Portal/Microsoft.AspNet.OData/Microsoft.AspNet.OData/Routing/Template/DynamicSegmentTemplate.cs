using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000081 RID: 129
	public class DynamicSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000F840 File Offset: 0x0000DA40
		public DynamicSegmentTemplate(DynamicPathSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
			this.PropertyName = segment.Identifier;
			this.TreatPropertyNameAsParameterName = false;
			if (RoutingConventionHelpers.IsRouteParameter(this.PropertyName))
			{
				this.PropertyName = this.PropertyName.Substring(1, this.PropertyName.Length - 2);
				this.TreatPropertyNameAsParameterName = true;
				if (string.IsNullOrEmpty(this.PropertyName))
				{
					throw new ODataException(Error.Format(SRResources.EmptyParameterAlias, new object[] { this.PropertyName, segment.Identifier }));
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000F8E3 File Offset: 0x0000DAE3
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000F8EB File Offset: 0x0000DAEB
		public DynamicPathSegment Segment { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		private string PropertyName { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000F905 File Offset: 0x0000DB05
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000F90D File Offset: 0x0000DB0D
		private bool TreatPropertyNameAsParameterName { get; set; }

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000F918 File Offset: 0x0000DB18
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			DynamicPathSegment dynamicPathSegment = pathSegment as DynamicPathSegment;
			if (dynamicPathSegment == null)
			{
				return false;
			}
			if (this.TreatPropertyNameAsParameterName)
			{
				values[this.PropertyName] = dynamicPathSegment.Identifier;
				values["DF908045-6922-46A0-82F2-2F6E7F43D1B1_" + this.PropertyName] = new ODataParameterValue(dynamicPathSegment.Identifier, EdmLibHelpers.GetEdmPrimitiveTypeReferenceOrNull(typeof(string)));
				return true;
			}
			return this.PropertyName == dynamicPathSegment.Identifier;
		}
	}
}
