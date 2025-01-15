using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.InfoNav.Defaults;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000019 RID: 25
	public static class ConceptualSchemaUtils
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public static XmlReader FillAssociationSetEndRoleAttribute(XmlReader reader, out Version csdlVersion)
		{
			XDocument xdocument = XDocument.Load(reader);
			XAttribute xattribute = xdocument.Root.Attribute(EdmConstants.Version);
			csdlVersion = ConceptualSchemaUtils.defaultVersion;
			if (xattribute != null && Version.TryParse(xattribute.Value, out csdlVersion) && csdlVersion >= ConceptualSchemaUtils.version25)
			{
				return xdocument.CreateReader();
			}
			foreach (XElement xelement in from entityContainer in xdocument.Root.Elements(EdmConstants.EntityContainer)
				from associationSet in entityContainer.Elements(EdmConstants.AssociationSet)
				select associationSet)
			{
				IList<XElement> list = (from e in xelement.Elements(EdmConstants.End)
					where e.Attribute(EdmConstants.RoleAttr) == null
					select e).Evaluate<XElement>();
				if (list.Count != 0)
				{
					string associationName = xelement.Attribute(EdmConstants.AssociationAttr).Value;
					string text = associationName.Substring(0, associationName.IndexOf('.') + 1);
					associationName = associationName.Substring(text.Length);
					XElement xelement2 = (from a in xdocument.Root.Elements(EdmConstants.Association)
						where (string)a.Attribute(EdmConstants.NameAttr) == associationName
						select a).FirstOrDefault<XElement>();
					if (xelement2 != null)
					{
						foreach (XElement xelement3 in list)
						{
							string entityTypeName = xelement3.Attribute(EdmConstants.EntitySetAttr).Value;
							entityTypeName = text + entityTypeName;
							XElement xelement4 = (from e in xelement2.Elements(EdmConstants.End)
								where (string)e.Attribute(EdmConstants.TypeAttr) == entityTypeName
								select e).FirstOrDefault<XElement>();
							if (xelement4 != null)
							{
								string value = xelement4.Attribute(EdmConstants.RoleAttr).Value;
								xelement3.SetAttributeValue(EdmConstants.RoleAttr, value);
							}
						}
					}
				}
			}
			return xdocument.CreateReader();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003D24 File Offset: 0x00001F24
		public static bool TryParseConceptualSchemaWithAssociationSetFill(Stream csdlUtf8Stream, ConceptualSchemaBuilderOptions conceptualSchemaBuilderOptions, ITracer tracer, out IConceptualSchema conceptualSchema, out IList<ConceptualSchemaLoadError> conceptualSchemaErrors)
		{
			if (tracer == null)
			{
				tracer = DefaultTracer.Instance;
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
			using (XmlReader xmlReader = XmlReader.Create(csdlUtf8Stream, xmlReaderSettings))
			{
				Version version;
				using (XmlReader xmlReader2 = ConceptualSchemaUtils.FillAssociationSetEndRoleAttribute(xmlReader, out version))
				{
					if (!ConceptualSchemaBuilder.TryCreateConceptualSchema(new XmlReader[] { xmlReader2 }, tracer, version, conceptualSchemaBuilderOptions, out conceptualSchema, out conceptualSchemaErrors))
					{
						tracer.TraceError("Failed to create ConceptualSchema from CSDL");
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x040000A8 RID: 168
		private static readonly Version version25 = new Version(2, 5);

		// Token: 0x040000A9 RID: 169
		private static readonly Version defaultVersion = new Version(1, 0);
	}
}
