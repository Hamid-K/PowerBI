using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000076 RID: 118
	public class ODataPathSegmentTranslator : PathSegmentTranslator<ODataPathSegment>
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public static IEnumerable<ODataPathSegment> Translate(IEdmModel model, ODataPath path, IDictionary<string, SingleValueNode> parameterAliasNodes)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (path == null)
			{
				throw Error.ArgumentNull("path");
			}
			ODataPathSegmentTranslator odataPathSegmentTranslator = new ODataPathSegmentTranslator(model, parameterAliasNodes);
			return path.WalkWith<ODataPathSegment>(odataPathSegmentTranslator);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000EF74 File Offset: 0x0000D174
		public ODataPathSegmentTranslator(IEdmModel model, IDictionary<string, SingleValueNode> parameterAliasNodes)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			this._model = model;
			this._parameterAliasNodes = parameterAliasNodes ?? new Dictionary<string, SingleValueNode>();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(TypeSegment segment)
		{
			return segment;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(EntitySetSegment segment)
		{
			return segment;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(SingletonSegment segment)
		{
			return segment;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(PropertySegment segment)
		{
			return segment;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(DynamicPathSegment segment)
		{
			return segment;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(CountSegment segment)
		{
			return segment;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(NavigationPropertySegment segment)
		{
			return segment;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(NavigationPropertyLinkSegment segment)
		{
			return segment;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(ValueSegment segment)
		{
			return segment;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(BatchSegment segment)
		{
			return segment;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(MetadataSegment segment)
		{
			return segment;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(PathTemplateSegment segment)
		{
			return segment;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000EFA1 File Offset: 0x0000D1A1
		public override ODataPathSegment Translate(KeySegment segment)
		{
			return segment;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public override ODataPathSegment Translate(OperationImportSegment segment)
		{
			if (segment.OperationImports.Single<IEdmOperationImport>() is IEdmActionImport)
			{
				return segment;
			}
			OperationImportSegment operationImportSegment = segment;
			if (segment.Parameters.Any((OperationSegmentParameter p) => p.Value is ParameterAliasNode || p.Value is ConvertNode))
			{
				IEnumerable<OperationSegmentParameter> enumerable = segment.Parameters.Select((OperationSegmentParameter e) => new OperationSegmentParameter(e.Name, this.TranslateNode(e.Value)));
				operationImportSegment = new OperationImportSegment(segment.OperationImports, segment.EntitySet, enumerable);
			}
			return operationImportSegment;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000F020 File Offset: 0x0000D220
		public override ODataPathSegment Translate(OperationSegment segment)
		{
			if (segment.Operations.Single<IEdmOperation>() is IEdmFunction)
			{
				OperationSegment operationSegment = segment;
				if (segment.Parameters.Any((OperationSegmentParameter p) => p.Value is ParameterAliasNode || p.Value is ConvertNode))
				{
					IEnumerable<OperationSegmentParameter> enumerable = segment.Parameters.Select((OperationSegmentParameter e) => new OperationSegmentParameter(e.Name, this.TranslateNode(e.Value)));
					operationSegment = new OperationSegment(segment.Operations, enumerable, segment.EntitySet);
				}
				segment = operationSegment;
			}
			ReturnedEntitySetAnnotation annotationValue = this._model.GetAnnotationValue(segment.Operations.Single<IEdmOperation>());
			IEdmEntitySet edmEntitySet = null;
			if (annotationValue != null)
			{
				edmEntitySet = this._model.EntityContainer.FindEntitySet(annotationValue.EntitySetName);
			}
			if (edmEntitySet != null)
			{
				segment = new OperationSegment(segment.Operations, segment.Parameters, edmEntitySet);
			}
			return segment;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		private object TranslateNode(object node)
		{
			if (node == null)
			{
				throw Error.ArgumentNull("node");
			}
			ConstantNode constantNode = node as ConstantNode;
			if (constantNode != null)
			{
				return constantNode;
			}
			ConvertNode convertNode = node as ConvertNode;
			if (convertNode != null)
			{
				object obj = this.TranslateNode(convertNode.Source);
				return this.ConvertNode(obj, convertNode.TypeReference);
			}
			ParameterAliasNode parameterAliasNode = node as ParameterAliasNode;
			if (parameterAliasNode == null)
			{
				throw Error.NotSupported(SRResources.CannotRecognizeNodeType, new object[]
				{
					typeof(ODataPathSegmentTranslator),
					node.GetType().FullName
				});
			}
			SingleValueNode singleValueNode;
			if (this._parameterAliasNodes.TryGetValue(parameterAliasNode.Alias, out singleValueNode) && singleValueNode != null)
			{
				return this.TranslateNode(singleValueNode);
			}
			return null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000F190 File Offset: 0x0000D390
		private object ConvertNode(object node, IEdmTypeReference typeReference)
		{
			if (node == null)
			{
				return null;
			}
			ConstantNode constantNode = node as ConstantNode;
			if (constantNode == null)
			{
				return node;
			}
			if (constantNode.Value is UriTemplateExpression)
			{
				return node;
			}
			if (constantNode.Value is ODataEnumValue)
			{
				return node;
			}
			string literalText = constantNode.LiteralText;
			return new ConstantNode(ODataUriUtils.ConvertFromUriLiteral(literalText, ODataVersion.V4, this._model, typeReference), literalText, typeReference);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		internal static SingleValueNode TranslateParameterAlias(SingleValueNode node, IDictionary<string, SingleValueNode> parameterAliasNodes)
		{
			if (node == null)
			{
				throw Error.ArgumentNull("node");
			}
			if (parameterAliasNodes == null)
			{
				throw Error.ArgumentNull("parameterAliasNodes");
			}
			ParameterAliasNode parameterAliasNode = node as ParameterAliasNode;
			if (parameterAliasNode == null)
			{
				return node;
			}
			SingleValueNode singleValueNode;
			if (parameterAliasNodes.TryGetValue(parameterAliasNode.Alias, out singleValueNode) && singleValueNode != null)
			{
				if (singleValueNode is ParameterAliasNode)
				{
					singleValueNode = ODataPathSegmentTranslator.TranslateParameterAlias(singleValueNode, parameterAliasNodes);
				}
				return singleValueNode;
			}
			return null;
		}

		// Token: 0x040000E7 RID: 231
		private readonly IDictionary<string, SingleValueNode> _parameterAliasNodes;

		// Token: 0x040000E8 RID: 232
		private readonly IEdmModel _model;
	}
}
