using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000074 RID: 116
	public static class ODataParameterHelper
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x0000E344 File Offset: 0x0000C544
		public static bool TryGetParameterValue(this OperationSegment segment, string parameterName, out object parameterValue)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			if (string.IsNullOrEmpty(parameterName))
			{
				throw Error.ArgumentNullOrEmpty("parameterName");
			}
			parameterValue = null;
			OperationSegmentParameter operationSegmentParameter = segment.Parameters.FirstOrDefault((OperationSegmentParameter p) => p.Name == parameterName);
			if (operationSegmentParameter == null)
			{
				return false;
			}
			parameterValue = ODataParameterHelper.TranslateNode(operationSegmentParameter.Value);
			return true;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public static object GetParameterValue(this OperationSegment segment, string parameterName)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			if (string.IsNullOrEmpty(parameterName))
			{
				throw Error.ArgumentNullOrEmpty("parameterName");
			}
			if (!segment.Operations.Any<IEdmOperation>() || !segment.Operations.First<IEdmOperation>().IsFunction())
			{
				throw Error.Argument("segment", SRResources.OperationSegmentMustBeFunction, new object[0]);
			}
			OperationSegmentParameter operationSegmentParameter = segment.Parameters.FirstOrDefault((OperationSegmentParameter p) => p.Name == parameterName);
			if (operationSegmentParameter == null)
			{
				throw Error.Argument("parameterName", SRResources.FunctionParameterNotFound, new object[] { parameterName });
			}
			object obj = ODataParameterHelper.TranslateNode(operationSegmentParameter.Value);
			if (obj == null || obj is ODataNullValue)
			{
				IEdmOperationParameter edmOperationParameter = segment.Operations.First<IEdmOperation>().Parameters.First((IEdmOperationParameter p) => p.Name == parameterName);
				if (!edmOperationParameter.Type.IsNullable)
				{
					throw new ODataException(string.Format(CultureInfo.InvariantCulture, SRResources.NullOnNonNullableFunctionParameter, new object[] { edmOperationParameter.Type.FullName() }));
				}
			}
			return obj;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		public static bool TryGetParameterValue(this OperationImportSegment segment, string parameterName, out object parameterValue)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			if (string.IsNullOrEmpty(parameterName))
			{
				throw Error.ArgumentNullOrEmpty("parameterName");
			}
			parameterValue = null;
			OperationSegmentParameter operationSegmentParameter = segment.Parameters.FirstOrDefault((OperationSegmentParameter p) => p.Name == parameterName);
			if (operationSegmentParameter == null)
			{
				return false;
			}
			parameterValue = ODataParameterHelper.TranslateNode(operationSegmentParameter.Value);
			return true;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000E544 File Offset: 0x0000C744
		public static object GetParameterValue(this OperationImportSegment segment, string parameterName)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			if (string.IsNullOrEmpty(parameterName))
			{
				throw Error.ArgumentNullOrEmpty("parameterName");
			}
			if (!segment.OperationImports.Any<IEdmOperationImport>() || !segment.OperationImports.First<IEdmOperationImport>().IsFunctionImport())
			{
				throw Error.Argument("segment", SRResources.OperationImportSegmentMustBeFunction, new object[0]);
			}
			OperationSegmentParameter operationSegmentParameter = segment.Parameters.FirstOrDefault((OperationSegmentParameter p) => p.Name == parameterName);
			if (operationSegmentParameter == null)
			{
				throw Error.Argument("parameterName", SRResources.FunctionParameterNotFound, new object[] { parameterName });
			}
			object obj = ODataParameterHelper.TranslateNode(operationSegmentParameter.Value);
			if (obj == null || obj is ODataNullValue)
			{
				IEdmOperationParameter edmOperationParameter = segment.OperationImports.First<IEdmOperationImport>().Operation.Parameters.First((IEdmOperationParameter p) => p.Name == parameterName);
				if (!edmOperationParameter.Type.IsNullable)
				{
					throw new ODataException(string.Format(CultureInfo.InvariantCulture, SRResources.NullOnNonNullableFunctionParameter, new object[] { edmOperationParameter.Type.FullName() }));
				}
			}
			return obj;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000E668 File Offset: 0x0000C868
		internal static object TranslateNode(object value)
		{
			if (value == null)
			{
				return null;
			}
			ConstantNode constantNode = value as ConstantNode;
			if (constantNode != null)
			{
				return constantNode.Value;
			}
			ConvertNode convertNode = value as ConvertNode;
			if (convertNode != null)
			{
				return ODataParameterHelper.TranslateNode(convertNode.Source);
			}
			ParameterAliasNode parameterAliasNode = value as ParameterAliasNode;
			if (parameterAliasNode != null)
			{
				return parameterAliasNode.Alias;
			}
			throw Error.NotSupported(SRResources.CannotRecognizeNodeType, new object[]
			{
				typeof(ODataParameterHelper),
				value.GetType().FullName
			});
		}
	}
}
