using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000082 RID: 130
	public class OperationSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000F994 File Offset: 0x0000DB94
		public OperationSegmentTemplate(OperationSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
			IEdmOperation edmOperation = this.Segment.Operations.First<IEdmOperation>();
			if (edmOperation.IsFunction())
			{
				this.ParameterMappings = RoutingConventionHelpers.BuildParameterMappings(segment.Parameters, edmOperation.FullName());
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public OperationSegment Segment { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000F9FD File Offset: 0x0000DBFD
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000FA05 File Offset: 0x0000DC05
		public IDictionary<string, string> ParameterMappings { get; private set; }

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			OperationSegment operationSegment = pathSegment as OperationSegment;
			if (operationSegment == null)
			{
				return false;
			}
			IEdmOperation edmOperation = this.Segment.Operations.First<IEdmOperation>();
			IEdmOperation edmOperation2 = operationSegment.Operations.First<IEdmOperation>();
			if (edmOperation.IsAction() && edmOperation2.IsAction())
			{
				return edmOperation == edmOperation2;
			}
			if (edmOperation.IsFunction() && edmOperation2.IsFunction())
			{
				if (edmOperation.FullName() != edmOperation2.FullName())
				{
					return false;
				}
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (OperationSegmentParameter operationSegmentParameter in operationSegment.Parameters)
				{
					object parameterValue = operationSegment.GetParameterValue(operationSegmentParameter.Name);
					dictionary[operationSegmentParameter.Name] = parameterValue;
				}
				if (RoutingConventionHelpers.TryMatch(this.ParameterMappings, dictionary, values))
				{
					foreach (OperationSegmentParameter operationSegmentParameter2 in operationSegment.Parameters)
					{
						string name = operationSegmentParameter2.Name;
						object obj = dictionary[name];
						RoutingConventionHelpers.AddFunctionParameters((IEdmFunction)edmOperation2, name, obj, values, values, this.ParameterMappings);
					}
					return true;
				}
			}
			return false;
		}
	}
}
