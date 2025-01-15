using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000083 RID: 131
	public class OperationImportSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public OperationImportSegmentTemplate(OperationImportSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
			IEdmOperationImport edmOperationImport = this.Segment.OperationImports.First<IEdmOperationImport>();
			if (edmOperationImport.IsFunctionImport())
			{
				this.ParameterMappings = RoutingConventionHelpers.BuildParameterMappings(segment.Parameters, edmOperationImport.Name);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000FBB4 File Offset: 0x0000DDB4
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		public OperationImportSegment Segment { get; private set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000FBC5 File Offset: 0x0000DDC5
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000FBCD File Offset: 0x0000DDCD
		public IDictionary<string, string> ParameterMappings { get; private set; }

		// Token: 0x060004CD RID: 1229 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			OperationImportSegment operationImportSegment = pathSegment as OperationImportSegment;
			if (operationImportSegment == null)
			{
				return false;
			}
			IEdmOperationImport edmOperationImport = this.Segment.OperationImports.First<IEdmOperationImport>();
			IEdmOperationImport edmOperationImport2 = operationImportSegment.OperationImports.First<IEdmOperationImport>();
			if (edmOperationImport.IsActionImport() && edmOperationImport2.IsActionImport())
			{
				return edmOperationImport == edmOperationImport2;
			}
			if (edmOperationImport.IsFunctionImport() && edmOperationImport2.IsFunctionImport())
			{
				if (edmOperationImport.Name != edmOperationImport2.Name)
				{
					return false;
				}
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (OperationSegmentParameter operationSegmentParameter in operationImportSegment.Parameters)
				{
					object parameterValue = operationImportSegment.GetParameterValue(operationSegmentParameter.Name);
					dictionary[operationSegmentParameter.Name] = parameterValue;
				}
				if (RoutingConventionHelpers.TryMatch(this.ParameterMappings, dictionary, values))
				{
					foreach (OperationSegmentParameter operationSegmentParameter2 in operationImportSegment.Parameters)
					{
						string name = operationSegmentParameter2.Name;
						object obj = dictionary[name];
						RoutingConventionHelpers.AddFunctionParameters((IEdmFunction)edmOperationImport2.Operation, name, obj, values, values, this.ParameterMappings);
					}
					return true;
				}
			}
			return false;
		}
	}
}
