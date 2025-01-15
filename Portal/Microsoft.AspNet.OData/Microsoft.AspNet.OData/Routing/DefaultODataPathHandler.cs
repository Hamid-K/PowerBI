using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing.Template;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200006F RID: 111
	public class DefaultODataPathHandler : IODataPathHandler, IODataPathTemplateHandler
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000D81D File Offset: 0x0000BA1D
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000D825 File Offset: 0x0000BA25
		public ODataUrlKeyDelimiter UrlKeyDelimiter { get; set; }

		// Token: 0x0600042C RID: 1068 RVA: 0x0000D82E File Offset: 0x0000BA2E
		public virtual ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer)
		{
			if (serviceRoot == null)
			{
				throw Error.ArgumentNull("serviceRoot");
			}
			if (odataPath == null)
			{
				throw Error.ArgumentNull("odataPath");
			}
			if (requestContainer == null)
			{
				throw Error.ArgumentNull("requestContainer");
			}
			return this.Parse(serviceRoot, odataPath, requestContainer, false);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D864 File Offset: 0x0000BA64
		public virtual ODataPathTemplate ParseTemplate(string odataPathTemplate, IServiceProvider requestContainer)
		{
			if (odataPathTemplate == null)
			{
				throw Error.ArgumentNull("odataPathTemplate");
			}
			if (requestContainer == null)
			{
				throw Error.ArgumentNull("requestContainer");
			}
			return DefaultODataPathHandler.Templatify(this.Parse(null, odataPathTemplate, requestContainer, true), odataPathTemplate);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000D892 File Offset: 0x0000BA92
		public virtual string Link(ODataPath path)
		{
			if (path == null)
			{
				throw Error.ArgumentNull("path");
			}
			return path.ToString();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		private ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer, bool template)
		{
			Uri uri = null;
			Uri uri2 = null;
			IEdmModel requiredService = ServiceProviderServiceExtensions.GetRequiredService<IEdmModel>(requestContainer);
			ODataUriParser odataUriParser;
			if (template)
			{
				odataUriParser = new ODataUriParser(requiredService, new Uri(odataPath, UriKind.Relative), requestContainer);
				odataUriParser.EnableUriTemplateParsing = true;
			}
			else
			{
				uri = new Uri(serviceRoot.EndsWith("/", StringComparison.Ordinal) ? serviceRoot : (serviceRoot + "/"));
				if (!Uri.TryCreate(odataPath, UriKind.Absolute, out uri2))
				{
					Uri uri3 = uri;
					uri2 = new Uri(((uri3 != null) ? uri3.ToString() : null) + odataPath);
				}
				odataUriParser = new ODataUriParser(requiredService, uri, uri2, requestContainer);
			}
			if (this.UrlKeyDelimiter != null)
			{
				odataUriParser.UrlKeyDelimiter = this.UrlKeyDelimiter;
			}
			else
			{
				odataUriParser.UrlKeyDelimiter = ODataUrlKeyDelimiter.Slash;
			}
			UnresolvedPathSegment unresolvedPathSegment = null;
			KeySegment keySegment = null;
			ODataPath odataPath2;
			try
			{
				odataPath2 = odataUriParser.ParsePath();
			}
			catch (ODataUnrecognizedPathException ex)
			{
				if (ex.ParsedSegments == null || !ex.ParsedSegments.Any<ODataPathSegment>() || (!(ex.ParsedSegments.Last<ODataPathSegment>().EdmType is IEdmComplexType) && !(ex.ParsedSegments.Last<ODataPathSegment>().EdmType is IEdmEntityType)) || !(ex.CurrentSegment != "$count"))
				{
					throw;
				}
				if (ex.UnparsedSegments.Any<string>())
				{
					throw new ODataException(Error.Format(SRResources.InvalidPathSegment, new object[]
					{
						ex.UnparsedSegments.First<string>(),
						ex.CurrentSegment
					}));
				}
				odataPath2 = new ODataPath(ex.ParsedSegments);
				unresolvedPathSegment = new UnresolvedPathSegment(ex.CurrentSegment);
			}
			if (!template && odataPath2.LastSegment is NavigationPropertyLinkSegment)
			{
				IEdmCollectionType edmCollectionType = odataPath2.LastSegment.EdmType as IEdmCollectionType;
				if (edmCollectionType != null)
				{
					EntityIdSegment entityIdSegment = null;
					bool flag = false;
					try
					{
						entityIdSegment = odataUriParser.ParseEntityId();
						if (entityIdSegment != null)
						{
							keySegment = new ODataUriParser(requiredService, uri, entityIdSegment.Id, requestContainer).ParsePath().LastSegment as KeySegment;
						}
					}
					catch (ODataException)
					{
						flag = true;
					}
					if (flag || (entityIdSegment != null && (keySegment == null || (!keySegment.EdmType.IsOrInheritsFrom(edmCollectionType.ElementType.Definition) && !edmCollectionType.ElementType.Definition.IsOrInheritsFrom(keySegment.EdmType)))))
					{
						string text = uri2.Query;
						string text2 = "$id=";
						int num = text.IndexOf(text2, StringComparison.OrdinalIgnoreCase);
						if (num >= 0)
						{
							int num2 = text.IndexOf("&", num, StringComparison.OrdinalIgnoreCase);
							if (num2 >= 0)
							{
								text = text.Substring(num + text2.Length, num2 - 1);
							}
							else
							{
								text = text.Substring(num + text2.Length);
							}
						}
						throw new ODataException(Error.Format(SRResources.InvalidDollarId, new object[] { text }));
					}
				}
			}
			odataPath2.WalkWith(new DefaultODataPathValidator(requiredService));
			List<ODataPathSegment> list = ODataPathSegmentTranslator.Translate(requiredService, odataPath2, odataUriParser.ParameterAliasNodes).ToList<ODataPathSegment>();
			if (unresolvedPathSegment != null)
			{
				list.Add(unresolvedPathSegment);
			}
			if (!template)
			{
				DefaultODataPathHandler.AppendIdForRef(list, keySegment);
			}
			return new ODataPath(list)
			{
				Path = odataPath2
			};
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		private static void AppendIdForRef(IList<ODataPathSegment> segments, KeySegment id)
		{
			if (id == null || !(segments.Last<ODataPathSegment>() is NavigationPropertyLinkSegment))
			{
				return;
			}
			segments.Add(id);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		private static ODataPathTemplate Templatify(ODataPath path, string pathTemplate)
		{
			if (path == null)
			{
				throw new ODataException(Error.Format(SRResources.InvalidODataPathTemplate, new object[] { pathTemplate }));
			}
			ODataPathSegmentTemplateTranslator translator = new ODataPathSegmentTemplateTranslator();
			return new ODataPathTemplate(path.Segments.Select(delegate(ODataPathSegment e)
			{
				UnresolvedPathSegment unresolvedPathSegment = e as UnresolvedPathSegment;
				if (unresolvedPathSegment != null)
				{
					throw new ODataException(Error.Format(SRResources.UnresolvedPathSegmentInTemplate, new object[] { unresolvedPathSegment, pathTemplate }));
				}
				return e.TranslateWith<ODataPathSegmentTemplate>(translator);
			}));
		}
	}
}
