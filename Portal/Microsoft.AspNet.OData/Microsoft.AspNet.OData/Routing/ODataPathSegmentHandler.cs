using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x02000075 RID: 117
	public class ODataPathSegmentHandler : PathSegmentHandler
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0000E6DD File Offset: 0x0000C8DD
		public ODataPathSegmentHandler()
		{
			this._navigationSource = null;
			this._pathTemplate = new List<string> { "~" };
			this._pathUriLiteral = new List<string>();
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000E70D File Offset: 0x0000C90D
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this._navigationSource;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000E715 File Offset: 0x0000C915
		public string PathTemplate
		{
			get
			{
				return string.Join("/", this._pathTemplate);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000E727 File Offset: 0x0000C927
		public string PathLiteral
		{
			get
			{
				return string.Join("/", this._pathUriLiteral);
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000E739 File Offset: 0x0000C939
		public override void Handle(EntitySetSegment segment)
		{
			this._navigationSource = segment.EntitySet;
			this._pathTemplate.Add("entityset");
			this._pathUriLiteral.Add(segment.EntitySet.Name);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000E770 File Offset: 0x0000C970
		public override void Handle(KeySegment segment)
		{
			this._navigationSource = segment.NavigationSource;
			if (this._pathTemplate.Last<string>() == "$ref")
			{
				this._pathTemplate.Insert(this._pathTemplate.Count - 1, "key");
			}
			else
			{
				this._pathTemplate.Add("key");
			}
			string text = ODataPathSegmentHandler.ConvertKeysToString(segment.Keys, segment.EdmType);
			if (!this._pathUriLiteral.Any<string>())
			{
				this._pathUriLiteral.Add("(" + text + ")");
				return;
			}
			if (this._pathUriLiteral.Last<string>() == "$ref")
			{
				this._pathUriLiteral[this._pathUriLiteral.Count - 2] = this._pathUriLiteral[this._pathUriLiteral.Count - 2] + "(" + text + ")";
				return;
			}
			this._pathUriLiteral[this._pathUriLiteral.Count - 1] = this._pathUriLiteral[this._pathUriLiteral.Count - 1] + "(" + text + ")";
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		public override void Handle(NavigationPropertyLinkSegment segment)
		{
			this._navigationSource = segment.NavigationSource;
			this._pathTemplate.Add("navigation");
			this._pathTemplate.Add("$ref");
			this._pathUriLiteral.Add(segment.NavigationProperty.Name);
			this._pathUriLiteral.Add("$ref");
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E903 File Offset: 0x0000CB03
		public override void Handle(NavigationPropertySegment segment)
		{
			this._navigationSource = segment.NavigationSource;
			this._pathTemplate.Add("navigation");
			this._pathUriLiteral.Add(segment.NavigationProperty.Name);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E937 File Offset: 0x0000CB37
		public override void Handle(DynamicPathSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add("dynamicproperty");
			this._pathUriLiteral.Add(segment.Identifier);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000E964 File Offset: 0x0000CB64
		public override void Handle(OperationImportSegment segment)
		{
			this._navigationSource = segment.EntitySet;
			IEdmActionImport edmActionImport = segment.OperationImports.Single<IEdmOperationImport>() as IEdmActionImport;
			if (edmActionImport != null)
			{
				this._pathTemplate.Add("unboundaction");
				this._pathUriLiteral.Add(edmActionImport.Name);
				return;
			}
			this._pathTemplate.Add("unboundfunction");
			IEnumerable<KeyValuePair<string, string>> enumerable = segment.Parameters.ToDictionary((OperationSegmentParameter parameterValue) => parameterValue.Name, (OperationSegmentParameter parameterValue) => ODataPathSegmentHandler.TranslateNode(parameterValue.Value));
			IEdmFunctionImport edmFunctionImport = (IEdmFunctionImport)segment.OperationImports.Single<IEdmOperationImport>();
			IEnumerable<string> enumerable2 = enumerable.Select((KeyValuePair<string, string> v) => string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[] { v.Key, v.Value }));
			string text = string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				edmFunctionImport.Name,
				string.Join(",", enumerable2)
			});
			this._pathUriLiteral.Add(text);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		public override void Handle(OperationSegment segment)
		{
			this._navigationSource = segment.EntitySet;
			IEdmAction edmAction = segment.Operations.Single<IEdmOperation>() as IEdmAction;
			if (edmAction != null)
			{
				this._pathTemplate.Add("action");
				this._pathUriLiteral.Add(edmAction.FullName());
				return;
			}
			this._pathTemplate.Add("function");
			IEnumerable<KeyValuePair<string, string>> enumerable = segment.Parameters.ToDictionary((OperationSegmentParameter parameterValue) => parameterValue.Name, (OperationSegmentParameter parameterValue) => ODataPathSegmentHandler.TranslateNode(parameterValue.Value));
			IEdmFunction edmFunction = (IEdmFunction)segment.Operations.Single<IEdmOperation>();
			IEnumerable<string> enumerable2 = enumerable.Select((KeyValuePair<string, string> v) => string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[] { v.Key, v.Value }));
			string text = string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				edmFunction.FullName(),
				string.Join(",", enumerable2)
			});
			this._pathUriLiteral.Add(text);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public override void Handle(PathTemplateSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add("property");
			this._pathUriLiteral.Add(segment.LiteralText);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000EBBD File Offset: 0x0000CDBD
		public override void Handle(PropertySegment segment)
		{
			this._pathTemplate.Add("property");
			this._pathUriLiteral.Add(segment.Property.Name);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
		public override void Handle(SingletonSegment segment)
		{
			this._navigationSource = segment.Singleton;
			this._pathTemplate.Add("singleton");
			this._pathUriLiteral.Add(segment.Singleton.Name);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		public override void Handle(TypeSegment segment)
		{
			this._navigationSource = segment.NavigationSource;
			this._pathTemplate.Add("cast");
			IEdmType edmType = segment.EdmType;
			if (segment.EdmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)segment.EdmType).ElementType.Definition;
			}
			this._pathUriLiteral.Add(edmType.FullTypeName());
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000EC81 File Offset: 0x0000CE81
		public override void Handle(ValueSegment segment)
		{
			this._pathTemplate.Add("$value");
			this._pathUriLiteral.Add("$value");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000ECA3 File Offset: 0x0000CEA3
		public override void Handle(CountSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add("$count");
			this._pathUriLiteral.Add("$count");
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000ECCC File Offset: 0x0000CECC
		public override void Handle(BatchSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add("$batch");
			this._pathUriLiteral.Add("$batch");
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000ECF5 File Offset: 0x0000CEF5
		public override void Handle(MetadataSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add("$metadata");
			this._pathUriLiteral.Add("$metadata");
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000ED1E File Offset: 0x0000CF1E
		public override void Handle(ODataPathSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add(segment.ToString());
			this._pathUriLiteral.Add(segment.ToString());
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000ED49 File Offset: 0x0000CF49
		public virtual void Handle(UnresolvedPathSegment segment)
		{
			this._navigationSource = null;
			this._pathTemplate.Add("unresolved");
			this._pathUriLiteral.Add(segment.SegmentValue);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000ED74 File Offset: 0x0000CF74
		private static string ConvertKeysToString(IEnumerable<KeyValuePair<string, object>> keys, IEdmType edmType)
		{
			IEdmEntityType edmEntityType = edmType as IEdmEntityType;
			IList<KeyValuePair<string, object>> list = (keys as IList<KeyValuePair<string, object>>) ?? keys.ToList<KeyValuePair<string, object>>();
			if (list.Count < 1)
			{
				return string.Empty;
			}
			if (list.Count < 2)
			{
				KeyValuePair<string, object> keyValue = list.First<KeyValuePair<string, object>>();
				if (edmEntityType.Key().Any((IEdmStructuralProperty k) => k.Name == keyValue.Key))
				{
					return string.Join(",", list.Select((KeyValuePair<string, object> keyValuePair) => ODataPathSegmentHandler.TranslateKeySegmentValue(keyValuePair.Value)).ToArray<string>());
				}
			}
			return string.Join(",", list.Select((KeyValuePair<string, object> keyValuePair) => keyValuePair.Key + "=" + ODataPathSegmentHandler.TranslateKeySegmentValue(keyValuePair.Value)).ToArray<string>());
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000EE48 File Offset: 0x0000D048
		private static string TranslateKeySegmentValue(object value)
		{
			if (value == null)
			{
				throw Error.ArgumentNull("value");
			}
			UriTemplateExpression uriTemplateExpression = value as UriTemplateExpression;
			if (uriTemplateExpression != null)
			{
				return uriTemplateExpression.LiteralText;
			}
			ConstantNode constantNode = value as ConstantNode;
			if (constantNode != null)
			{
				ODataEnumValue odataEnumValue = constantNode.Value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					return ODataUriUtils.ConvertToUriLiteral(odataEnumValue, ODataVersion.V4);
				}
			}
			return ODataUriUtils.ConvertToUriLiteral(value, ODataVersion.V4);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000EE9C File Offset: 0x0000D09C
		private static string TranslateNode(object node)
		{
			ConstantNode constantNode = node as ConstantNode;
			if (constantNode != null)
			{
				UriTemplateExpression uriTemplateExpression = constantNode.Value as UriTemplateExpression;
				if (uriTemplateExpression != null)
				{
					return uriTemplateExpression.LiteralText;
				}
				ODataEnumValue odataEnumValue = constantNode.Value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					return ODataUriUtils.ConvertToUriLiteral(odataEnumValue, ODataVersion.V4);
				}
				return constantNode.LiteralText;
			}
			else
			{
				ConvertNode convertNode = node as ConvertNode;
				if (convertNode != null)
				{
					return ODataPathSegmentHandler.TranslateNode(convertNode.Source);
				}
				ParameterAliasNode parameterAliasNode = node as ParameterAliasNode;
				if (parameterAliasNode != null)
				{
					return parameterAliasNode.Alias;
				}
				throw Error.NotSupported(SRResources.CannotRecognizeNodeType, new object[]
				{
					typeof(ODataPathSegmentHandler),
					node.GetType().FullName
				});
			}
		}

		// Token: 0x040000E4 RID: 228
		private readonly IList<string> _pathTemplate;

		// Token: 0x040000E5 RID: 229
		private readonly IList<string> _pathUriLiteral;

		// Token: 0x040000E6 RID: 230
		private IEdmNavigationSource _navigationSource;
	}
}
