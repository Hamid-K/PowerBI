using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Defaults;
using Microsoft.InfoNav.Utils;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200017B RID: 379
	public static class LinguisticSchemaUpgrader
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		static LinguisticSchemaUpgrader()
		{
			StateMachineDefinitionBuilder<LinguisticSchemaVersion, LinguisticSchemaUpgrader.Inputs, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext> stateMachineDefinitionBuilder = new StateMachineDefinitionBuilder<LinguisticSchemaVersion, LinguisticSchemaUpgrader.Inputs, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>();
			stateMachineDefinitionBuilder.AddTransition(LinguisticSchemaVersion.V000, LinguisticSchemaUpgrader.Inputs.Upgrade, LinguisticSchemaVersion.V001, new Action<LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>[]
			{
				new Action<LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>(LinguisticSchemaUpgrader.V000ToV001Upgrader.Upgrade)
			});
			stateMachineDefinitionBuilder.AddTransition(LinguisticSchemaVersion.V001, LinguisticSchemaUpgrader.Inputs.Upgrade, LinguisticSchemaVersion.V002, new Action<LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>[]
			{
				new Action<LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>(LinguisticSchemaUpgrader.V001ToV002Upgrader.Upgrade)
			});
			stateMachineDefinitionBuilder.AddTransition(LinguisticSchemaVersion.V002, LinguisticSchemaUpgrader.Inputs.Upgrade, LinguisticSchemaVersion.V003, new Action<LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>[]
			{
				new Action<LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>(LinguisticSchemaUpgrader.V002ToV003Upgrader.Upgrade)
			});
			LinguisticSchemaUpgrader.Definition = stateMachineDefinitionBuilder.GetDefinition();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000C633 File Offset: 0x0000A833
		internal static bool TryUpgrade(XmlReader reader, IConceptualSchema conceptualSchema, IErrorContext errorContext, ITracer tracer, ILinguisticSchemaUpgradeLanguageService languageService, out XDocument schema, out bool upgraded)
		{
			return LinguisticSchemaUpgrader.TryUpgrade(reader, LinguisticSchemaUpgrader.ToFunc(conceptualSchema), errorContext, tracer, languageService, out schema, out upgraded);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000C64C File Offset: 0x0000A84C
		internal static bool TryUpgrade(XDocument document, IConceptualSchema conceptualSchema, IErrorContext errorContext, ITracer tracer, ILinguisticSchemaUpgradeLanguageService languageService, out XDocument schema)
		{
			bool flag;
			return LinguisticSchemaUpgrader.TryUpgrade(document, LinguisticSchemaUpgrader.ToFunc(conceptualSchema), errorContext, tracer, languageService, out schema, out flag);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000C66D File Offset: 0x0000A86D
		internal static bool TryUpgrade(XmlReader reader, Func<IConceptualSchema> getConceptualSchema, IErrorContext errorContext, ITracer tracer, ILinguisticSchemaUpgradeLanguageService languageService, out XDocument upgradedSchema, out bool upgraded)
		{
			return LinguisticSchemaUpgrader.TryUpgrade(XDocument.Load(reader), getConceptualSchema, errorContext, tracer, languageService, out upgradedSchema, out upgraded);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000C684 File Offset: 0x0000A884
		internal static bool TryUpgrade(XDocument schema, Func<IConceptualSchema> getConceptualSchema, IErrorContext errorContext, ITracer tracer, ILinguisticSchemaUpgradeLanguageService languageService, out XDocument upgradedSchema, out bool upgraded)
		{
			LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext linguisticSchemaUpgradeContext = new LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext(schema, getConceptualSchema, errorContext, tracer, languageService);
			if (linguisticSchemaUpgradeContext.Schema.Root == null || linguisticSchemaUpgradeContext.Schema.Root.Name.LocalName != "LinguisticSchema")
			{
				linguisticSchemaUpgradeContext.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaMissingRootElement);
				upgradedSchema = null;
				upgraded = false;
				return false;
			}
			XAttribute xmlnsAttribute = linguisticSchemaUpgradeContext.Schema.Root.GetXmlnsAttribute();
			if (xmlnsAttribute == null)
			{
				linguisticSchemaUpgradeContext.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaUnknownVersion);
				upgradedSchema = null;
				upgraded = false;
				return false;
			}
			LinguisticSchemaVersion linguisticSchemaVersion = LinguisticSchemaVersionInformation.DetermineVersion(xmlnsAttribute.Value);
			if (linguisticSchemaVersion == LinguisticSchemaVersion.Unknown)
			{
				linguisticSchemaUpgradeContext.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaUnknownVersion);
				upgradedSchema = null;
				upgraded = false;
				return false;
			}
			if (!LinguisticSchemaUpgrader.TryUpgradeViaStateMachine(linguisticSchemaUpgradeContext, linguisticSchemaVersion, out upgradedSchema, out upgraded))
			{
				return false;
			}
			upgraded |= LinguisticSchemaUpgrader.SchemaLanguageUpgrader.Upgrade(linguisticSchemaUpgradeContext);
			upgraded |= LinguisticSchemaUpgrader.NounsToWordsUpgrader.Upgrade(linguisticSchemaUpgradeContext);
			upgraded |= LinguisticSchemaUpgrader.LinguisticRelationshipConditionUpgrader.Upgrade(linguisticSchemaUpgradeContext);
			upgraded |= LinguisticSchemaUpgrader.HiddenUpgrader.Upgrade(linguisticSchemaUpgradeContext);
			upgraded |= LinguisticSchemaUpgrader.SuggestedTermUpgrader.Upgrade(linguisticSchemaUpgradeContext);
			return true;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000C774 File Offset: 0x0000A974
		internal static XDocument Upgrade(XmlReader reader, Func<IConceptualSchema> getConceptualSchema, ITracer tracer = null)
		{
			bool flag;
			return LinguisticSchemaUpgrader.Upgrade(reader, getConceptualSchema, out flag, tracer);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000C78C File Offset: 0x0000A98C
		public static XDocument Upgrade(XmlReader reader, Func<IConceptualSchema> getConceptualSchema, out bool upgraded, ITracer tracer = null)
		{
			if (tracer == null)
			{
				tracer = DefaultTracer.Instance;
			}
			LinguisticSchemaUpgrader.DefaultErrorContext defaultErrorContext = new LinguisticSchemaUpgrader.DefaultErrorContext();
			XDocument xdocument;
			if (!LinguisticSchemaUpgrader.TryUpgrade(reader, getConceptualSchema, defaultErrorContext, tracer, null, out xdocument, out upgraded))
			{
				defaultErrorContext.SanitizedTrace(tracer);
				if (defaultErrorContext.HasError)
				{
					throw defaultErrorContext.CreateException();
				}
			}
			return xdocument;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		private static bool TryUpgradeViaStateMachine(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context, LinguisticSchemaVersion version, out XDocument upgradedSchema, out bool upgraded)
		{
			if (version == LinguisticSchemaUpgrader.LatestSupportedVersion)
			{
				upgradedSchema = context.Schema;
				upgraded = false;
				return true;
			}
			context.Tracer.SanitizedTraceInformation("Upgrading Linguistic Schema: {0}", new string[] { context.Schema.Root.GetXmlnsAttribute().Value });
			context.Schema.Root.OverrideDefaultXmlNamespace(LinguisticSchemaVersionInformation.LatestNamespace);
			StateMachine<LinguisticSchemaVersion, LinguisticSchemaUpgrader.Inputs, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext> stateMachine = new StateMachine<LinguisticSchemaVersion, LinguisticSchemaUpgrader.Inputs, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext>(LinguisticSchemaUpgrader.Definition, context, version);
			while (stateMachine.State != LinguisticSchemaUpgrader.LatestSupportedVersion)
			{
				stateMachine.PerformTransition(LinguisticSchemaUpgrader.Inputs.Upgrade);
				if (context.Schema == null)
				{
					upgradedSchema = null;
					upgraded = false;
					return false;
				}
			}
			upgradedSchema = context.Schema;
			upgraded = true;
			return true;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private static Func<IConceptualSchema> ToFunc(IConceptualSchema conceptualSchema)
		{
			if (conceptualSchema != null)
			{
				return () => conceptualSchema;
			}
			return null;
		}

		// Token: 0x040006E3 RID: 1763
		internal static readonly LinguisticSchemaVersion LatestSupportedVersion = LinguisticSchemaVersionInformation.DetermineVersion(LinguisticSchemaVersionInformation.LatestNamespace);

		// Token: 0x040006E4 RID: 1764
		internal static readonly StateMachineDefinition<LinguisticSchemaVersion, LinguisticSchemaUpgrader.Inputs, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext> Definition;

		// Token: 0x02000213 RID: 531
		private sealed class DynamicImprovementUpgrader
		{
			// Token: 0x06000B63 RID: 2915 RVA: 0x0001525B File Offset: 0x0001345B
			private DynamicImprovementUpgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000B64 RID: 2916 RVA: 0x0001526A File Offset: 0x0001346A
			internal static bool Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				return new LinguisticSchemaUpgrader.DynamicImprovementUpgrader(context).Upgrade();
			}

			// Token: 0x06000B65 RID: 2917 RVA: 0x00015278 File Offset: 0x00013478
			private bool Upgrade()
			{
				XAttribute xattribute = this._context.Schema.Root.Attribute(LinguisticSchemaUpgrader.SchemaConstants.DynamicImprovementAttr);
				if (xattribute == null || xattribute.Value == "HighConfidence")
				{
					this._context.Schema.Root.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.DynamicImprovementAttr, "Default");
					return true;
				}
				return false;
			}

			// Token: 0x04000855 RID: 2133
			private const string HighConfidenceValue = "HighConfidence";

			// Token: 0x04000856 RID: 2134
			private const string DefaultValue = "Default";

			// Token: 0x04000857 RID: 2135
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}

		// Token: 0x02000214 RID: 532
		[ImmutableObject(true)]
		private struct EdmElementKey : IEquatable<LinguisticSchemaUpgrader.EdmElementKey>, ICheckable
		{
			// Token: 0x06000B66 RID: 2918 RVA: 0x000152D7 File Offset: 0x000134D7
			internal EdmElementKey(string entity, string property = null)
			{
				this.Entity = entity;
				this.Property = property;
			}

			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000152E7 File Offset: 0x000134E7
			internal bool IsTable
			{
				get
				{
					return this.Property == null;
				}
			}

			// Token: 0x1700033B RID: 827
			// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000152F2 File Offset: 0x000134F2
			public bool IsValid
			{
				get
				{
					return !string.IsNullOrEmpty(this.Entity);
				}
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x00015302 File Offset: 0x00013502
			public bool Equals(LinguisticSchemaUpgrader.EdmElementKey other)
			{
				return EdmNameComparer.Instance.Equals(this.Entity, other.Entity) && EdmNameComparer.Instance.Equals(this.Property, other.Property);
			}

			// Token: 0x06000B6A RID: 2922 RVA: 0x00015334 File Offset: 0x00013534
			public override bool Equals(object obj)
			{
				return obj is LinguisticSchemaUpgrader.EdmElementKey && this.Equals((LinguisticSchemaUpgrader.EdmElementKey)obj);
			}

			// Token: 0x06000B6B RID: 2923 RVA: 0x0001534C File Offset: 0x0001354C
			public override int GetHashCode()
			{
				return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Entity, EdmNameComparer.Instance), Hashing.GetHashCode<string>(this.Property, EdmNameComparer.Instance));
			}

			// Token: 0x04000858 RID: 2136
			internal string Entity;

			// Token: 0x04000859 RID: 2137
			internal string Property;
		}

		// Token: 0x02000215 RID: 533
		private sealed class HiddenUpgrader
		{
			// Token: 0x06000B6C RID: 2924 RVA: 0x00015373 File Offset: 0x00013573
			private HiddenUpgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x00015382 File Offset: 0x00013582
			internal static bool Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				return new LinguisticSchemaUpgrader.HiddenUpgrader(context).Upgrade();
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x00015390 File Offset: 0x00013590
			private bool Upgrade()
			{
				XElement xelement = this._context.Schema.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem);
				if (xelement == null)
				{
					return false;
				}
				bool flag = false;
				foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.EntityElem))
				{
					flag |= LinguisticSchemaUpgrader.HiddenUpgrader.UpgradeVisibility(xelement2);
				}
				return flag;
			}

			// Token: 0x06000B6F RID: 2927 RVA: 0x00015408 File Offset: 0x00013608
			private static bool UpgradeVisibility(XElement entity)
			{
				XAttribute xattribute = entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.HiddenAttr);
				if (xattribute == null)
				{
					return false;
				}
				entity.SetElementValue(LinguisticSchemaUpgrader.SchemaConstants.VisibilityElem, LinguisticSchemaUpgrader.HiddenUpgrader.GetVisibility(xattribute.Value, LinguisticSchemaUpgrader.HiddenUpgrader.IsStructuralEntity(entity)));
				entity.RemoveAttributes(new XName[] { LinguisticSchemaUpgrader.SchemaConstants.HiddenAttr });
				return true;
			}

			// Token: 0x06000B70 RID: 2928 RVA: 0x00015458 File Offset: 0x00013658
			private static string GetVisibility(string value, bool isTableEntity)
			{
				if (!XmlConvert.ToBoolean(value))
				{
					return EntityVisibility.Visible.ToString();
				}
				if (!isTableEntity)
				{
					return EntityVisibility.Hidden.ToString();
				}
				return EntityVisibility.Children.ToString();
			}

			// Token: 0x06000B71 RID: 2929 RVA: 0x000154A0 File Offset: 0x000136A0
			private static bool IsStructuralEntity(XElement entity)
			{
				return entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.ConceptualEntityAttr) != null && entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.ConceptualPropertyAttr) == null && entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.ConceptualHierarchyAttr) == null && entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.ConceptualHierarchyLevelAttr) == null && entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.ConceptualVariationSourceAttr) == null && entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.ConceptualVariationSetAttr) == null;
			}

			// Token: 0x0400085A RID: 2138
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}

		// Token: 0x02000216 RID: 534
		private sealed class LinguisticRelationshipConditionUpgrader
		{
			// Token: 0x06000B72 RID: 2930 RVA: 0x000154FE File Offset: 0x000136FE
			private LinguisticRelationshipConditionUpgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x0001550D File Offset: 0x0001370D
			internal static bool Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				return new LinguisticSchemaUpgrader.LinguisticRelationshipConditionUpgrader(context).Upgrade();
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x0001551C File Offset: 0x0001371C
			private bool Upgrade()
			{
				XElement xelement = this._context.Schema.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.RelationshipsElem);
				if (xelement == null)
				{
					return false;
				}
				bool flag = false;
				foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.RelationshipElem))
				{
					flag |= LinguisticSchemaUpgrader.LinguisticRelationshipConditionUpgrader.UpgradeCondition(xelement2);
				}
				return flag;
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x00015594 File Offset: 0x00013794
			private static bool UpgradeCondition(XElement relationship)
			{
				XElement xelement = relationship.Element(LinguisticSchemaUpgrader.SchemaConstants.ConditionElem);
				if (xelement == null)
				{
					return false;
				}
				XAttribute xattribute = xelement.Attribute(LinguisticSchemaUpgrader.SchemaConstants.EntityAttr);
				if (xattribute == null)
				{
					return false;
				}
				XElement xelement2 = relationship.Element(LinguisticSchemaUpgrader.SchemaConstants.RolesElem);
				if (xelement2 == null)
				{
					xelement2 = new XElement(LinguisticSchemaUpgrader.SchemaConstants.RolesElem);
					relationship.AddFirst(xelement2);
				}
				string value = xattribute.Value;
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				string text = null;
				foreach (XElement xelement3 in xelement2.Elements())
				{
					if (LinguisticObjectNameComparer.Instance.Equals(xelement3.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.EntityAttr), value))
					{
						text = xelement3.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr);
						break;
					}
					hashSet.Add(xelement3.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr));
				}
				if (text == null)
				{
					XElement xelement4 = new XElement(LinguisticSchemaUpgrader.SchemaConstants.RoleElem);
					text = StringUtil.MakeUniqueName(value, hashSet);
					xelement4.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.NameAttr, text);
					xelement4.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.EntityAttr, value);
					xelement2.Add(xelement4);
				}
				XElement xelement5 = new XElement(LinguisticSchemaUpgrader.SchemaConstants.TargetElement);
				xelement5.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.RoleAttr, text);
				xelement.AddFirst(xelement5);
				xelement.RemoveAttributes(new XName[]
				{
					LinguisticSchemaUpgrader.SchemaConstants.EntityAttr,
					LinguisticSchemaUpgrader.SchemaConstants.EntityNamespaceAttr
				});
				return true;
			}

			// Token: 0x0400085B RID: 2139
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}

		// Token: 0x02000217 RID: 535
		internal sealed class LinguisticSchemaUpgradeContext
		{
			// Token: 0x06000B76 RID: 2934 RVA: 0x000156F4 File Offset: 0x000138F4
			internal LinguisticSchemaUpgradeContext(XDocument schema, Func<IConceptualSchema> getConceptualSchema, IErrorContext errorContext, ITracer tracer, ILinguisticSchemaUpgradeLanguageService languageService)
			{
				this._schema = schema;
				this._getConceptualSchema = getConceptualSchema;
				this._errorContext = errorContext;
				this._tracer = tracer;
				this._languageService = languageService ?? new LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext.DefaultLanguageService();
			}

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0001572A File Offset: 0x0001392A
			// (set) Token: 0x06000B78 RID: 2936 RVA: 0x00015732 File Offset: 0x00013932
			internal XDocument Schema
			{
				get
				{
					return this._schema;
				}
				set
				{
					this._schema = value;
				}
			}

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0001573B File Offset: 0x0001393B
			internal IConceptualSchema ConceptualSchema
			{
				get
				{
					if (this._conceptualSchema == null && this._getConceptualSchema != null)
					{
						this._conceptualSchema = this._getConceptualSchema();
						this._getConceptualSchema = null;
					}
					return this._conceptualSchema;
				}
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0001576B File Offset: 0x0001396B
			internal ITracer Tracer
			{
				get
				{
					return this._tracer;
				}
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00015773 File Offset: 0x00013973
			internal ILinguisticSchemaUpgradeLanguageService LanguageService
			{
				get
				{
					return this._languageService;
				}
			}

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001577B File Offset: 0x0001397B
			internal bool HasError
			{
				get
				{
					return this._errorContext.HasError;
				}
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x00015788 File Offset: 0x00013988
			internal void RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode errorCode)
			{
				this._errorContext.RegisterError("{0}", new object[] { errorCode });
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x000157A9 File Offset: 0x000139A9
			internal void RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode errorCode, string messageTemplate, params object[] args)
			{
				this._errorContext.RegisterError("{0}: {1}", new object[]
				{
					errorCode,
					FormattableStringFactory.Create(messageTemplate, args)
				});
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x000157D4 File Offset: 0x000139D4
			internal void RegisterWarning(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode errorCode, string messageTemplate, params object[] args)
			{
				this._errorContext.RegisterWarning("{0}: {1}", new object[]
				{
					errorCode,
					FormattableStringFactory.Create(messageTemplate, args)
				});
			}

			// Token: 0x0400085C RID: 2140
			private readonly IErrorContext _errorContext;

			// Token: 0x0400085D RID: 2141
			private readonly ITracer _tracer;

			// Token: 0x0400085E RID: 2142
			private readonly ILinguisticSchemaUpgradeLanguageService _languageService;

			// Token: 0x0400085F RID: 2143
			private Func<IConceptualSchema> _getConceptualSchema;

			// Token: 0x04000860 RID: 2144
			private IConceptualSchema _conceptualSchema;

			// Token: 0x04000861 RID: 2145
			private XDocument _schema;

			// Token: 0x02000257 RID: 599
			private sealed class DefaultLanguageService : ILinguisticSchemaUpgradeLanguageService
			{
				// Token: 0x06000CD3 RID: 3283 RVA: 0x0001AB1C File Offset: 0x00018D1C
				public bool IsValid(LanguageIdentifier language)
				{
					bool flag;
					try
					{
						language.ToCultureInfo();
						flag = true;
					}
					catch (CultureNotFoundException)
					{
						flag = false;
					}
					return flag;
				}

				// Token: 0x06000CD4 RID: 3284 RVA: 0x0001AB4C File Offset: 0x00018D4C
				public string MakeFriendlyNameString(string name, LanguageIdentifier language)
				{
					return name;
				}
			}
		}

		// Token: 0x02000218 RID: 536
		internal enum LinguisticSchemaUpgradeErrorCode
		{
			// Token: 0x04000863 RID: 2147
			LinguisticSchemaMissingRootElement,
			// Token: 0x04000864 RID: 2148
			LinguisticSchemaUnknownVersion,
			// Token: 0x04000865 RID: 2149
			LinguisticSchemaMissingEntityName,
			// Token: 0x04000866 RID: 2150
			LinguisticSchemaMissingEntitySet,
			// Token: 0x04000867 RID: 2151
			LinguisticSchemaMissingLanguage,
			// Token: 0x04000868 RID: 2152
			LinguisticSchemaInvalidLanguage,
			// Token: 0x04000869 RID: 2153
			LinguisticSchemaMissingWords,
			// Token: 0x0400086A RID: 2154
			LinguisticSchemaMissingConditionProperty,
			// Token: 0x0400086B RID: 2155
			LinguisticSchemaInvalidConditionValue,
			// Token: 0x0400086C RID: 2156
			LinguisticSchemaDuplicateEntityNames
		}

		// Token: 0x02000219 RID: 537
		internal enum Inputs
		{
			// Token: 0x0400086E RID: 2158
			Upgrade
		}

		// Token: 0x0200021A RID: 538
		private sealed class DefaultErrorContext : IErrorContext
		{
			// Token: 0x06000B80 RID: 2944 RVA: 0x000157FF File Offset: 0x000139FF
			internal DefaultErrorContext()
			{
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00015807 File Offset: 0x00013A07
			public bool HasError
			{
				get
				{
					return this._errors != null && this._errors.Count > 0;
				}
			}

			// Token: 0x06000B82 RID: 2946 RVA: 0x00015821 File Offset: 0x00013A21
			void IErrorContext.RegisterError(string messageTemplate, params object[] args)
			{
				if (this._errors == null)
				{
					this._errors = new List<string>(1);
				}
				this._errors.Add(StringUtil.FormatInvariant(messageTemplate, args));
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x00015849 File Offset: 0x00013A49
			void IErrorContext.RegisterWarning(string messageTemplate, params object[] args)
			{
				if (this._warnings == null)
				{
					this._warnings = new List<string>(1);
				}
				this._warnings.Add(StringUtil.FormatInvariant(messageTemplate, args));
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x00015874 File Offset: 0x00013A74
			internal void SanitizedTrace(ITracer tracer)
			{
				if (this._errors != null)
				{
					foreach (string text in this._errors)
					{
						tracer.SanitizedTraceError(text, Array.Empty<string>());
					}
				}
				if (this._warnings != null)
				{
					foreach (string text2 in this._warnings)
					{
						tracer.SanitizedTraceWarning(text2, Array.Empty<string>());
					}
				}
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x00015924 File Offset: 0x00013B24
			internal Exception CreateException()
			{
				return Contract.Except("Linguistic schema upgrade failed.{0}{1}", new object[]
				{
					Environment.NewLine,
					this._errors.StringJoin(null)
				});
			}

			// Token: 0x0400086F RID: 2159
			private List<string> _errors;

			// Token: 0x04000870 RID: 2160
			private List<string> _warnings;
		}

		// Token: 0x0200021B RID: 539
		private static class LinguisticSchemaUpgradeUtil
		{
			// Token: 0x06000B86 RID: 2950 RVA: 0x00015950 File Offset: 0x00013B50
			internal static bool TryGetValidLanguage(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context, out LanguageIdentifier language)
			{
				XAttribute xattribute = context.Schema.Root.Attribute(LinguisticSchemaUpgrader.SchemaConstants.LanguageAttr);
				if (xattribute == null)
				{
					context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaMissingLanguage);
					language = (LanguageIdentifier)0;
					return false;
				}
				if (!LanguageIdentifierUtil.TryAsLanguageIdentifier(xattribute.Value, out language) || !context.LanguageService.IsValid(language))
				{
					context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaInvalidLanguage);
					language = (LanguageIdentifier)0;
					return false;
				}
				return true;
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x000159AC File Offset: 0x00013BAC
			internal static Dictionary<LinguisticSchemaUpgrader.EdmElementKey, XElement> MergeDuplicateEntities(XElement entities, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				Dictionary<LinguisticSchemaUpgrader.EdmElementKey, XElement> dictionary = new Dictionary<LinguisticSchemaUpgrader.EdmElementKey, XElement>();
				List<XElement> list = new List<XElement>();
				foreach (XElement xelement in entities.Elements(LinguisticSchemaUpgrader.SchemaConstants.EntityElem))
				{
					LinguisticSchemaUpgrader.EdmElementKey edmElementKey = LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.CreateEdmElementKey(xelement);
					if (edmElementKey.IsValid)
					{
						XElement xelement2;
						if (dictionary.TryGetValue(edmElementKey, out xelement2))
						{
							bool flag = true;
							string attribute = xelement2.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr);
							if (LinguisticObjectNameComparer.Instance.Equals(xelement.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr), attribute))
							{
								context.RegisterWarning(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaDuplicateEntityNames, "Entity name '{0}' is non-unique. Conflicting occurrences will be removed.", new object[] { attribute.ToScrubbedString() });
								LinguisticItemSource entitySource = LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.GetEntitySource(xelement);
								LinguisticItemSource entitySource2 = LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.GetEntitySource(xelement2);
								switch (entitySource)
								{
								case LinguisticItemSource.User:
									flag = entitySource2 == LinguisticItemSource.User || entitySource2 == LinguisticItemSource.Generated;
									break;
								case LinguisticItemSource.Generated:
									flag = entitySource2 == LinguisticItemSource.Generated;
									break;
								}
								if (flag)
								{
									list.Add(xelement2);
								}
								else
								{
									list.Add(xelement);
								}
							}
							if (flag)
							{
								dictionary[edmElementKey] = xelement;
							}
						}
						else
						{
							dictionary.Add(edmElementKey, xelement);
						}
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					list[i].Remove();
				}
				return dictionary;
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x00015B00 File Offset: 0x00013D00
			internal static LinguisticItemSource GetEntitySource(XElement entity)
			{
				XAttribute xattribute = entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SourceAttr);
				if (xattribute == null || xattribute.Value == LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.User)
				{
					return LinguisticItemSource.User;
				}
				if (xattribute.Value == LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.Generated)
				{
					return LinguisticItemSource.Generated;
				}
				if (xattribute.Value == LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.Deleted)
				{
					return LinguisticItemSource.Deleted;
				}
				throw Contract.ExceptNotSupported("Unsupported LinguisticItemSource value: " + xattribute.Value);
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x00015B6D File Offset: 0x00013D6D
			internal static void UpgradeEntitySource(XElement entity)
			{
				if (entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SourceAttr) != null)
				{
					return;
				}
				if (entity.Element(LinguisticSchemaUpgrader.SchemaConstants.NounsElem) != null)
				{
					return;
				}
				entity.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.SourceAttr, LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.Generated);
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x00015B9C File Offset: 0x00013D9C
			internal static LinguisticSchemaUpgrader.EdmElementKey CreateEdmElementKey(XElement entity)
			{
				XAttribute xattribute = entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.EdmEntitySetAttr);
				if (xattribute == null)
				{
					return default(LinguisticSchemaUpgrader.EdmElementKey);
				}
				XAttribute xattribute2 = entity.Attribute(LinguisticSchemaUpgrader.SchemaConstants.EdmPropertyAttr);
				return new LinguisticSchemaUpgrader.EdmElementKey(xattribute.Value, (xattribute2 != null) ? xattribute2.Value : null);
			}

			// Token: 0x04000871 RID: 2161
			private static readonly string Generated = LinguisticItemSource.Generated.ToString();

			// Token: 0x04000872 RID: 2162
			private static readonly string User = LinguisticItemSource.User.ToString();

			// Token: 0x04000873 RID: 2163
			private static readonly string Deleted = LinguisticItemSource.Deleted.ToString();
		}

		// Token: 0x0200021C RID: 540
		private sealed class NounsToWordsUpgrader
		{
			// Token: 0x06000B8C RID: 2956 RVA: 0x00015C31 File Offset: 0x00013E31
			private NounsToWordsUpgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x00015C40 File Offset: 0x00013E40
			internal static bool Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				return new LinguisticSchemaUpgrader.NounsToWordsUpgrader(context).Upgrade();
			}

			// Token: 0x06000B8E RID: 2958 RVA: 0x00015C50 File Offset: 0x00013E50
			private bool Upgrade()
			{
				XElement xelement = this._context.Schema.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem);
				bool flag = false;
				if (xelement != null)
				{
					foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.EntityElem))
					{
						flag |= LinguisticSchemaUpgrader.NounsToWordsUpgrader.ConvertNounsToWords(xelement2);
					}
				}
				return flag;
			}

			// Token: 0x06000B8F RID: 2959 RVA: 0x00015CC8 File Offset: 0x00013EC8
			private static bool ConvertNounsToWords(XElement entity)
			{
				XElement xelement = entity.Element(LinguisticSchemaUpgrader.SchemaConstants.NounsElem);
				if (xelement == null)
				{
					return false;
				}
				XElement xelement2 = entity.Element(LinguisticSchemaUpgrader.SchemaConstants.WordsElem);
				if (xelement2 == null)
				{
					xelement2 = new XElement(LinguisticSchemaUpgrader.SchemaConstants.WordsElem);
					entity.AddFirst(xelement2);
				}
				foreach (XElement xelement3 in xelement.Elements())
				{
					XElement xelement4 = new XElement(LinguisticSchemaUpgrader.SchemaConstants.WordElem);
					xelement4.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.TypeAttr, WordType.Noun);
					xelement4.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.SourceAttr, LinguisticSchemaSource.User);
					XAttribute xattribute = xelement3.Attribute(LinguisticSchemaUpgrader.SchemaConstants.WeightAttr);
					if (xattribute != null)
					{
						xelement4.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.WeightAttr, xattribute.Value);
					}
					xelement4.Value = xelement3.Value;
					xelement2.Add(xelement4);
				}
				xelement.Remove();
				return true;
			}

			// Token: 0x04000874 RID: 2164
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}

		// Token: 0x0200021D RID: 541
		internal static class SchemaConstants
		{
			// Token: 0x04000875 RID: 2165
			internal static readonly XNamespace Namespace = LinguisticSchemaVersionInformation.LatestNamespace;

			// Token: 0x04000876 RID: 2166
			internal static readonly XName LanguageAttr = "Language";

			// Token: 0x04000877 RID: 2167
			internal static readonly XName DynamicImprovementAttr = "DynamicImprovement";

			// Token: 0x04000878 RID: 2168
			internal static readonly XName MinResultConfidenceAttr = "MinResultConfidence";

			// Token: 0x04000879 RID: 2169
			internal static readonly XName SourceAttr = "Source";

			// Token: 0x0400087A RID: 2170
			internal static readonly XName SourceTypeAttr = "SourceType";

			// Token: 0x0400087B RID: 2171
			internal static readonly XName SourceAgentAttr = "SourceAgent";

			// Token: 0x0400087C RID: 2172
			internal static readonly XName EntitiesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Entities";

			// Token: 0x0400087D RID: 2173
			internal static readonly XName EntityElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Entity";

			// Token: 0x0400087E RID: 2174
			internal static readonly XName NameAttr = "Name";

			// Token: 0x0400087F RID: 2175
			internal static readonly XName ConceptualEntityAttr = "ConceptualEntity";

			// Token: 0x04000880 RID: 2176
			internal static readonly XName ConceptualPropertyAttr = "ConceptualProperty";

			// Token: 0x04000881 RID: 2177
			internal static readonly XName ConceptualHierarchyAttr = "ConceptualHierarchy";

			// Token: 0x04000882 RID: 2178
			internal static readonly XName ConceptualHierarchyLevelAttr = "ConceptualHierarchyLevel";

			// Token: 0x04000883 RID: 2179
			internal static readonly XName ConceptualVariationSourceAttr = "ConceptualVariationSource";

			// Token: 0x04000884 RID: 2180
			internal static readonly XName ConceptualVariationSetAttr = "ConceptualVariationSet";

			// Token: 0x04000885 RID: 2181
			internal static readonly XName TextDefinitionAttr = "TextDefinition";

			// Token: 0x04000886 RID: 2182
			internal static readonly XName TemplateSchemaAttr = "TemplateSchema";

			// Token: 0x04000887 RID: 2183
			internal static readonly XName HiddenAttr = "Hidden";

			// Token: 0x04000888 RID: 2184
			internal static readonly XName WordsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Words";

			// Token: 0x04000889 RID: 2185
			internal static readonly XName WordElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Word";

			// Token: 0x0400088A RID: 2186
			internal static readonly XName NounsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Nouns";

			// Token: 0x0400088B RID: 2187
			internal static readonly XName NounElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Noun";

			// Token: 0x0400088C RID: 2188
			internal static readonly XName VisibilityElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Visibility";

			// Token: 0x0400088D RID: 2189
			internal static readonly XName InstanceWeightsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "InstanceWeights";

			// Token: 0x0400088E RID: 2190
			internal static readonly XName InstanceSynonymsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "InstanceSynonyms";

			// Token: 0x0400088F RID: 2191
			internal static readonly XName InstanceIndexElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "InstanceIndex";

			// Token: 0x04000890 RID: 2192
			internal static readonly XName InstancePluralNormalizationElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "InstancePluralNormalization";

			// Token: 0x04000891 RID: 2193
			internal static readonly XName WordConceptualPropertyAttr = "WordConceptualProperty";

			// Token: 0x04000892 RID: 2194
			internal static readonly XName ValueConceptualPropertyAttr = "ValueConceptualProperty";

			// Token: 0x04000893 RID: 2195
			internal static readonly XName UnitsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Units";

			// Token: 0x04000894 RID: 2196
			internal static readonly XName UnitElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Unit";

			// Token: 0x04000895 RID: 2197
			internal static readonly XName RelationshipsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Relationships";

			// Token: 0x04000896 RID: 2198
			internal static readonly XName RelationshipElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Relationship";

			// Token: 0x04000897 RID: 2199
			internal static readonly XName RolesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Roles";

			// Token: 0x04000898 RID: 2200
			internal static readonly XName RoleElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Role";

			// Token: 0x04000899 RID: 2201
			internal static readonly XName PathElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Path";

			// Token: 0x0400089A RID: 2202
			internal static readonly XName SegmentElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Segment";

			// Token: 0x0400089B RID: 2203
			internal static readonly XName ConceptualNavigationPropertyAttr = "ConceptualNavigationProperty";

			// Token: 0x0400089C RID: 2204
			internal static readonly XName ConditionElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Condition";

			// Token: 0x0400089D RID: 2205
			internal static readonly XName OperatorElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Operator";

			// Token: 0x0400089E RID: 2206
			internal static readonly XName StringValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "StringValue";

			// Token: 0x0400089F RID: 2207
			internal static readonly XName NumberValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "NumberValue";

			// Token: 0x040008A0 RID: 2208
			internal static readonly XName IntegerValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "IntegerValue";

			// Token: 0x040008A1 RID: 2209
			internal static readonly XName BooleanValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "BooleanValue";

			// Token: 0x040008A2 RID: 2210
			internal static readonly XName DateTimeValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "DateTimeValue";

			// Token: 0x040008A3 RID: 2211
			internal static readonly XName NullValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Null";

			// Token: 0x040008A4 RID: 2212
			internal static readonly XName PhrasingsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Phrasings";

			// Token: 0x040008A5 RID: 2213
			internal static readonly XName AttributePhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "AttributePhrasing";

			// Token: 0x040008A6 RID: 2214
			internal static readonly XName AdjectivePhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "AdjectivePhrasing";

			// Token: 0x040008A7 RID: 2215
			internal static readonly XName DynamicAdjectivePhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "DynamicAdjectivePhrasing";

			// Token: 0x040008A8 RID: 2216
			internal static readonly XName NounPhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "NounPhrasing";

			// Token: 0x040008A9 RID: 2217
			internal static readonly XName DynamicNounPhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "DynamicNounPhrasing";

			// Token: 0x040008AA RID: 2218
			internal static readonly XName NamePhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "NamePhrasing";

			// Token: 0x040008AB RID: 2219
			internal static readonly XName PrepositionPhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "PrepositionPhrasing";

			// Token: 0x040008AC RID: 2220
			internal static readonly XName VerbPhrasingElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "VerbPhrasing";

			// Token: 0x040008AD RID: 2221
			internal static readonly XName AdjectiveElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Adjective";

			// Token: 0x040008AE RID: 2222
			internal static readonly XName EntityAttr = "Entity";

			// Token: 0x040008AF RID: 2223
			internal static readonly XName EntityNamespaceAttr = "EntityNamespace";

			// Token: 0x040008B0 RID: 2224
			internal static readonly XName TypeAttr = "Type";

			// Token: 0x040008B1 RID: 2225
			internal static readonly XName WeightAttr = "Weight";

			// Token: 0x040008B2 RID: 2226
			internal static readonly XName TargetElement = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Target";

			// Token: 0x040008B3 RID: 2227
			internal static readonly XName RoleAttr = "Role";

			// Token: 0x040008B4 RID: 2228
			internal static readonly XName SchemaReferencesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "SchemaReferences";

			// Token: 0x040008B5 RID: 2229
			internal static readonly XName SchemaReferenceElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "SchemaReference";

			// Token: 0x040008B6 RID: 2230
			internal static readonly XName SchemaReferenceNamespaceAttr = "Namespace";

			// Token: 0x040008B7 RID: 2231
			internal static readonly XName StateAttr = "State";

			// Token: 0x040008B8 RID: 2232
			internal static readonly XName SemanticTypeElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "SemanticType";

			// Token: 0x040008B9 RID: 2233
			internal static readonly XName NameTypeElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "NameType";

			// Token: 0x040008BA RID: 2234
			internal static readonly XName GlobalSynonymsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "GlobalSynonyms";

			// Token: 0x040008BB RID: 2235
			internal static readonly XName GlobalSynonymElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "GlobalSynonym";

			// Token: 0x040008BC RID: 2236
			internal static readonly XName OriginalTermElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "OriginalTerm";

			// Token: 0x040008BD RID: 2237
			internal static readonly XName TargetTermElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "TargetTerm";

			// Token: 0x040008BE RID: 2238
			internal static readonly XName ExamplesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Examples";

			// Token: 0x040008BF RID: 2239
			internal static readonly XName ExampleElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Example";

			// Token: 0x040008C0 RID: 2240
			internal static readonly XName WhereElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Where";

			// Token: 0x040008C1 RID: 2241
			internal static readonly XName WhenElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "When";

			// Token: 0x040008C2 RID: 2242
			internal static readonly XName DurationElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Duration";

			// Token: 0x040008C3 RID: 2243
			internal static readonly XName OccurrencesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Occurrences";

			// Token: 0x040008C4 RID: 2244
			internal static readonly XName QuantityElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Quantity";

			// Token: 0x040008C5 RID: 2245
			internal static readonly XName AmountElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Amount";

			// Token: 0x040008C6 RID: 2246
			internal static readonly XName AggregationElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Aggregation";

			// Token: 0x040008C7 RID: 2247
			internal static readonly XName SubjectElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Subject";

			// Token: 0x040008C8 RID: 2248
			internal static readonly XName VerbsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Verbs";

			// Token: 0x040008C9 RID: 2249
			internal static readonly XName VerbElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Verb";

			// Token: 0x040008CA RID: 2250
			internal static readonly XName ObjectElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Object";

			// Token: 0x040008CB RID: 2251
			internal static readonly XName IndirectObjectElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "IndirectObject";

			// Token: 0x040008CC RID: 2252
			internal static readonly XName AdjectivesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Adjectives";

			// Token: 0x040008CD RID: 2253
			internal static readonly XName AntonymsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Antonyms";

			// Token: 0x040008CE RID: 2254
			internal static readonly XName AntonymElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Antonym";

			// Token: 0x040008CF RID: 2255
			internal static readonly XName MeasurementElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Measurement";

			// Token: 0x040008D0 RID: 2256
			internal static readonly XName PrepositionalPhrasesElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "PrepositionalPhrases";

			// Token: 0x040008D1 RID: 2257
			internal static readonly XName PrepositionalPhraseElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "PrepositionalPhrase";

			// Token: 0x040008D2 RID: 2258
			internal static readonly XName PrepositionsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Prepositions";

			// Token: 0x040008D3 RID: 2259
			internal static readonly XName PrepositionElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Preposition";

			// Token: 0x040008D4 RID: 2260
			internal static readonly XName NameElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Name";

			// Token: 0x040008D5 RID: 2261
			internal static readonly XName LastModifiedAttr = "LastModified";

			// Token: 0x040008D6 RID: 2262
			internal static readonly XName EdmEntitySetAttr = "EdmEntitySet";

			// Token: 0x040008D7 RID: 2263
			internal static readonly XName EdmPropertyAttr = "EdmProperty";

			// Token: 0x040008D8 RID: 2264
			internal static readonly XName WordEdmPropertyAttr = "WordEdmProperty";

			// Token: 0x040008D9 RID: 2265
			internal static readonly XName ValueEdmPropertyAttr = "ValueEdmProperty";

			// Token: 0x040008DA RID: 2266
			internal static readonly XName EdmNavigationPropertyAttr = "EdmNavigationProperty";
		}

		// Token: 0x0200021E RID: 542
		private sealed class SchemaLanguageUpgrader
		{
			// Token: 0x06000B91 RID: 2961 RVA: 0x00016510 File Offset: 0x00014710
			private SchemaLanguageUpgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x0001651F File Offset: 0x0001471F
			internal static bool Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				return new LinguisticSchemaUpgrader.SchemaLanguageUpgrader(context).Upgrade();
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x0001652C File Offset: 0x0001472C
			private bool Upgrade()
			{
				XAttribute xattribute = this._context.Schema.Root.Attribute(LinguisticSchemaUpgrader.SchemaConstants.LanguageAttr);
				LanguageIdentifier languageIdentifier;
				if (xattribute != null && LanguageIdentifierUtil.TryAsLanguageIdentifier(xattribute.Value, out languageIdentifier) && languageIdentifier == LanguageIdentifier.en)
				{
					xattribute.SetValue("en-US");
					return true;
				}
				return false;
			}

			// Token: 0x040008DB RID: 2267
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}

		// Token: 0x0200021F RID: 543
		private sealed class SuggestedTermUpgrader
		{
			// Token: 0x06000B94 RID: 2964 RVA: 0x00016579 File Offset: 0x00014779
			private SuggestedTermUpgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x00016588 File Offset: 0x00014788
			internal static bool Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				return new LinguisticSchemaUpgrader.SuggestedTermUpgrader(context).Upgrade();
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x00016598 File Offset: 0x00014798
			private bool Upgrade()
			{
				XElement xelement = this._context.Schema.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem);
				bool flag = false;
				if (xelement != null)
				{
					foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.EntityElem).Where(delegate(XElement e)
					{
						XAttribute xattribute = e.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SourceAttr);
						return ((xattribute != null) ? xattribute.Value : null) == "Deleted";
					}))
					{
						flag |= LinguisticSchemaUpgrader.SuggestedTermUpgrader.RemoveInternalSuggestedTerms(xelement2);
					}
				}
				return flag;
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x00016634 File Offset: 0x00014834
			private static bool RemoveInternalSuggestedTerms(XElement entity)
			{
				XElement xelement = entity.Element(LinguisticSchemaUpgrader.SchemaConstants.WordsElem);
				if (xelement == null)
				{
					return false;
				}
				List<XElement> list = xelement.Elements().Where(delegate(XElement e)
				{
					XAttribute xattribute = e.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SourceAttr);
					if (!(((xattribute != null) ? xattribute.Value : null) == "Suggested"))
					{
						return false;
					}
					if (e.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SourceTypeAttr) != null)
					{
						XAttribute xattribute2 = e.Attribute(LinguisticSchemaUpgrader.SchemaConstants.SourceTypeAttr);
						return ((xattribute2 != null) ? xattribute2.Value : null) == "Internal";
					}
					return true;
				}).ToList<XElement>();
				if (list.IsNullOrEmpty<XElement>())
				{
					return false;
				}
				foreach (XElement xelement2 in list)
				{
					xelement2.Remove();
				}
				return true;
			}

			// Token: 0x040008DC RID: 2268
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}

		// Token: 0x02000220 RID: 544
		private sealed class V000ToV001Upgrader
		{
			// Token: 0x06000B98 RID: 2968 RVA: 0x000166CC File Offset: 0x000148CC
			private V000ToV001Upgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
				this._root = this._context.Schema.Root;
			}

			// Token: 0x06000B99 RID: 2969 RVA: 0x000166F1 File Offset: 0x000148F1
			internal static void Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				new LinguisticSchemaUpgrader.V000ToV001Upgrader(context).Upgrade();
			}

			// Token: 0x06000B9A RID: 2970 RVA: 0x000166FE File Offset: 0x000148FE
			private void Upgrade()
			{
				this.AddStandardXmlNamespaces();
				this.OverrideLanguageAttribute();
				this.RemovePatternCompletionData();
				this.UpdatePhrasings();
			}

			// Token: 0x06000B9B RID: 2971 RVA: 0x00016718 File Offset: 0x00014918
			private void AddStandardXmlNamespaces()
			{
				this._root.SetAttributeValue(LinguisticSchemaUpgrader.V000ToV001Upgrader.XsiNamespace, "http://www.w3.org/2001/XMLSchema-instance");
				this._root.SetAttributeValue(LinguisticSchemaUpgrader.V000ToV001Upgrader.XsdNamespace, "http://www.w3.org/2001/XMLSchema");
			}

			// Token: 0x06000B9C RID: 2972 RVA: 0x00016744 File Offset: 0x00014944
			private void OverrideLanguageAttribute()
			{
				bool flag = true;
				XAttribute xattribute = this._root.Attribute(LinguisticSchemaUpgrader.SchemaConstants.LanguageAttr);
				LanguageIdentifier languageIdentifier;
				if (xattribute != null && LanguageIdentifierUtil.TryAsLanguageIdentifier(xattribute.Value, out languageIdentifier) && (languageIdentifier == LanguageIdentifier.en || languageIdentifier == LanguageIdentifier.en_US))
				{
					flag = false;
				}
				if (flag)
				{
					this._root.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.LanguageAttr, "en-US");
				}
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x000167A0 File Offset: 0x000149A0
			private void RemovePatternCompletionData()
			{
				XElement xelement = this._root.Element(LinguisticSchemaUpgrader.V000ToV001Upgrader.PatternCompletionDataElem);
				if (xelement != null)
				{
					xelement.Remove();
				}
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x000167C8 File Offset: 0x000149C8
			private void UpdatePhrasings()
			{
				XElement xelement = this._root.Element(LinguisticSchemaUpgrader.SchemaConstants.RelationshipsElem);
				if (xelement != null)
				{
					foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.RelationshipElem).Elements(LinguisticSchemaUpgrader.SchemaConstants.PhrasingsElem).Elements<XElement>())
					{
						if (xelement2.Name == LinguisticSchemaUpgrader.SchemaConstants.NounPhrasingElem)
						{
							if (xelement2.Element(LinguisticSchemaUpgrader.SchemaConstants.NounElem) != null)
							{
								xelement2.Name = LinguisticSchemaUpgrader.SchemaConstants.DynamicNounPhrasingElem;
							}
						}
						else if (xelement2.Name == LinguisticSchemaUpgrader.SchemaConstants.AdjectivePhrasingElem && xelement2.Element(LinguisticSchemaUpgrader.SchemaConstants.AdjectiveElem) != null)
						{
							xelement2.Name = LinguisticSchemaUpgrader.SchemaConstants.DynamicAdjectivePhrasingElem;
						}
					}
				}
			}

			// Token: 0x040008DD RID: 2269
			private static readonly XName PatternCompletionDataElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "PatternCompletionData";

			// Token: 0x040008DE RID: 2270
			private static readonly XName XsdNamespace = XNamespace.Xmlns + "xsd";

			// Token: 0x040008DF RID: 2271
			private static readonly XName XsiNamespace = XNamespace.Xmlns + "xsi";

			// Token: 0x040008E0 RID: 2272
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;

			// Token: 0x040008E1 RID: 2273
			private readonly XElement _root;
		}

		// Token: 0x02000221 RID: 545
		private sealed class V001ToV002Upgrader
		{
			// Token: 0x06000BA0 RID: 2976 RVA: 0x000168CE File Offset: 0x00014ACE
			private V001ToV002Upgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
				this._tableNames = new Dictionary<LinguisticSchemaUpgrader.EdmElementKey, string>();
				this._entityNames = new HashSet<string>(LinguisticObjectNameComparer.Instance);
				this._entitiesByEdm = new Dictionary<LinguisticSchemaUpgrader.EdmElementKey, XElement>();
			}

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00016903 File Offset: 0x00014B03
			private XElement Root
			{
				get
				{
					return this._context.Schema.Root;
				}
			}

			// Token: 0x06000BA2 RID: 2978 RVA: 0x00016915 File Offset: 0x00014B15
			internal static void Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				new LinguisticSchemaUpgrader.V001ToV002Upgrader(context).Upgrade();
				if (context.HasError)
				{
					context.Schema = null;
				}
			}

			// Token: 0x06000BA3 RID: 2979 RVA: 0x00016934 File Offset: 0x00014B34
			private void Upgrade()
			{
				if (!LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.TryGetValidLanguage(this._context, out this._language))
				{
					return;
				}
				this.Root.RemoveAttributes(new XName[] { LinguisticSchemaUpgrader.SchemaConstants.SourceAttr });
				this.DisableDynamicImprovementIfManuallyAuthored();
				XElement orAddFirst = this.Root.GetOrAddFirst(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem);
				if (!this.TryMigrateEdmElementsToEntities(orAddFirst))
				{
					return;
				}
				if (!this.TryMigrateConditions(orAddFirst))
				{
					return;
				}
				if (!orAddFirst.HasElements)
				{
					orAddFirst.Remove();
				}
			}

			// Token: 0x06000BA4 RID: 2980 RVA: 0x000169A8 File Offset: 0x00014BA8
			private void DisableDynamicImprovementIfManuallyAuthored()
			{
				if (this.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem) == null || this.Root.Element(LinguisticSchemaUpgrader.V001ToV002Upgrader.EdmElementsElem) != null)
				{
					return;
				}
				if (this.Root.Attribute(LinguisticSchemaUpgrader.SchemaConstants.DynamicImprovementAttr) != null)
				{
					return;
				}
				this.Root.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.DynamicImprovementAttr, ModelDynamicImprovement.None.ToString());
			}

			// Token: 0x06000BA5 RID: 2981 RVA: 0x00016A0C File Offset: 0x00014C0C
			private bool TryMigrateEdmElementsToEntities(XElement entitiesElem)
			{
				this._entitiesByEdm = LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.MergeDuplicateEntities(entitiesElem, this._context);
				foreach (KeyValuePair<LinguisticSchemaUpgrader.EdmElementKey, XElement> keyValuePair in this._entitiesByEdm)
				{
					LinguisticSchemaUpgrader.EdmElementKey key = keyValuePair.Key;
					XAttribute xattribute = keyValuePair.Value.Attribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr);
					if (xattribute == null || string.IsNullOrWhiteSpace(xattribute.Value))
					{
						this._context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaMissingEntityName);
						return false;
					}
					string value = xattribute.Value;
					this._entityNames.Add(value);
					if (key.IsTable)
					{
						this._tableNames.Add(key, value);
					}
				}
				XElement xelement = this.Root.Element(LinguisticSchemaUpgrader.V001ToV002Upgrader.EdmElementsElem);
				if (xelement != null)
				{
					foreach (XElement xelement2 in LinguisticSchemaUpgrader.V001ToV002Upgrader.GetOrderedEdmElements(xelement))
					{
						this.MigrateEdmElement(xelement2, entitiesElem);
					}
					xelement.Remove();
				}
				return true;
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x00016B3C File Offset: 0x00014D3C
			private static IEnumerable<XElement> GetOrderedEdmElements(XElement edmElements)
			{
				return from <>h__TransparentIdentifier1 in (from e in edmElements.Elements(LinguisticSchemaUpgrader.V001ToV002Upgrader.EdmElementElem)
						let entitySetAttr = e.Attribute(LinguisticSchemaUpgrader.SchemaConstants.EdmEntitySetAttr)
						select new
						{
							<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
							propertyAttr = e.Attribute(LinguisticSchemaUpgrader.SchemaConstants.EdmPropertyAttr)
						}).OrderBy(delegate(<>h__TransparentIdentifier1)
					{
						if (<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.entitySetAttr == null)
						{
							return null;
						}
						return <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.entitySetAttr.Value;
					}).ThenBy(delegate(<>h__TransparentIdentifier1)
					{
						if (<>h__TransparentIdentifier1.propertyAttr == null)
						{
							return null;
						}
						return <>h__TransparentIdentifier1.propertyAttr.Value;
					})
					select <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.e;
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x00016C08 File Offset: 0x00014E08
			private void MigrateEdmElement(XElement edmElement, XElement entitiesElem)
			{
				LinguisticSchemaUpgrader.EdmElementKey edmElementKey = LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.CreateEdmElementKey(edmElement);
				if (!edmElementKey.IsValid)
				{
					this._context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaMissingEntitySet);
					return;
				}
				XElement xelement = edmElement.Element(LinguisticSchemaUpgrader.SchemaConstants.WordsElem);
				if (xelement == null)
				{
					this._context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaMissingWords);
					return;
				}
				string text = (string)xelement.Element(LinguisticSchemaUpgrader.SchemaConstants.WordElem);
				XElement orCreateEntity = this.GetOrCreateEntity(edmElementKey, entitiesElem, text);
				if (orCreateEntity.Element(LinguisticSchemaUpgrader.SchemaConstants.WordsElem) != null)
				{
					return;
				}
				orCreateEntity.AddFirst(xelement);
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x00016C80 File Offset: 0x00014E80
			private bool TryMigrateConditions(XElement entitiesElem)
			{
				foreach (XElement xelement in this.Root.Descendants(LinguisticSchemaUpgrader.SchemaConstants.ConditionElem))
				{
					if (!this.TryMigrateConditionEdmElementReference(xelement, entitiesElem) || !this.TryMigrateConditionValue(xelement))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000BA9 RID: 2985 RVA: 0x00016CEC File Offset: 0x00014EEC
			private bool TryMigrateConditionEdmElementReference(XElement condition, XElement entitiesElem)
			{
				LinguisticSchemaUpgrader.EdmElementKey edmElementKey = LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.CreateEdmElementKey(condition);
				if (!edmElementKey.IsValid || edmElementKey.IsTable)
				{
					this._context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaMissingConditionProperty);
					return false;
				}
				string value = this.GetOrCreateEntity(edmElementKey, entitiesElem, null).Attribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr).Value;
				condition.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.EntityAttr, value);
				condition.RemoveAttributes(new XName[]
				{
					LinguisticSchemaUpgrader.SchemaConstants.EdmEntitySetAttr,
					LinguisticSchemaUpgrader.SchemaConstants.EdmPropertyAttr
				});
				return true;
			}

			// Token: 0x06000BAA RID: 2986 RVA: 0x00016D64 File Offset: 0x00014F64
			private bool TryMigrateConditionValue(XElement condition)
			{
				XElement xelement = condition.Element(LinguisticSchemaUpgrader.V001ToV002Upgrader.ValueElem);
				if (xelement == null)
				{
					this._context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaInvalidConditionValue);
					return false;
				}
				string typeStringFromTypeAttribute = LinguisticSchemaUpgrader.V001ToV002Upgrader.GetTypeStringFromTypeAttribute(xelement);
				XName xname;
				if (!(typeStringFromTypeAttribute == "string"))
				{
					if (!(typeStringFromTypeAttribute == "int") && !(typeStringFromTypeAttribute == "integer") && !(typeStringFromTypeAttribute == "long"))
					{
						if (!(typeStringFromTypeAttribute == "boolean"))
						{
							this._context.RegisterError(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeErrorCode.LinguisticSchemaInvalidConditionValue);
							return false;
						}
						xname = LinguisticSchemaUpgrader.SchemaConstants.BooleanValueElem;
					}
					else
					{
						xname = LinguisticSchemaUpgrader.SchemaConstants.IntegerValueElem;
					}
				}
				else
				{
					xname = LinguisticSchemaUpgrader.SchemaConstants.StringValueElem;
				}
				xelement.ReplaceWith(new XElement(xname, xelement.Value));
				return true;
			}

			// Token: 0x06000BAB RID: 2987 RVA: 0x00016E14 File Offset: 0x00015014
			private XElement GetOrCreateEntity(LinguisticSchemaUpgrader.EdmElementKey edmElementKey, XElement entitiesElem, string candidateName = null)
			{
				XElement xelement;
				if (!this._entitiesByEdm.TryGetValue(edmElementKey, out xelement))
				{
					if (candidateName == null)
					{
						string text = (edmElementKey.IsTable ? LinguisticEntityNameUtil.RemoveEntityContainer(edmElementKey.Entity) : edmElementKey.Property);
						candidateName = this._context.LanguageService.MakeFriendlyNameString(text, this._language);
					}
					string text2;
					if (edmElementKey.IsTable)
					{
						text2 = LinguisticEntityNameUtil.CreateAndRegisterEntityName(edmElementKey.Entity, candidateName, this._entityNames);
					}
					else
					{
						text2 = LinguisticEntityNameUtil.CreateAndRegisterEntityName(this.GetContainingTableLinguisticName(edmElementKey.Entity), edmElementKey.Property, candidateName, this._entityNames);
					}
					xelement = new XElement(LinguisticSchemaUpgrader.SchemaConstants.EntityElem, new object[]
					{
						new XAttribute(LinguisticSchemaUpgrader.SchemaConstants.NameAttr, text2),
						new XAttribute(LinguisticSchemaUpgrader.SchemaConstants.EdmEntitySetAttr, edmElementKey.Entity)
					});
					if (edmElementKey.Property != null)
					{
						xelement.Add(new XAttribute(LinguisticSchemaUpgrader.SchemaConstants.EdmPropertyAttr, edmElementKey.Property));
					}
					entitiesElem.Add(xelement);
					this._entitiesByEdm.Add(edmElementKey, xelement);
					if (edmElementKey.IsTable)
					{
						this._tableNames.Add(edmElementKey, text2);
					}
				}
				return xelement;
			}

			// Token: 0x06000BAC RID: 2988 RVA: 0x00016F28 File Offset: 0x00015128
			private string GetContainingTableLinguisticName(string edmElementEntity)
			{
				LinguisticSchemaUpgrader.EdmElementKey edmElementKey = new LinguisticSchemaUpgrader.EdmElementKey(edmElementEntity, null);
				string text;
				if (!this._tableNames.TryGetValue(edmElementKey, out text))
				{
					string text2 = LinguisticEntityNameUtil.RemoveEntityContainer(edmElementKey.Entity);
					string text3 = this._context.LanguageService.MakeFriendlyNameString(text2, this._language);
					text = LinguisticEntityNameUtil.CreateAndRegisterEntityName(text2, text3, this._entityNames);
					this._tableNames.Add(edmElementKey, text);
				}
				return text;
			}

			// Token: 0x06000BAD RID: 2989 RVA: 0x00016F90 File Offset: 0x00015190
			private static string GetTypeStringFromTypeAttribute(XElement element)
			{
				XAttribute xattribute = element.Attribute(LinguisticSchemaUpgrader.V001ToV002Upgrader.XsiTypeAttr);
				if (xattribute == null)
				{
					return "string";
				}
				string value = xattribute.Value;
				int num = value.IndexOf(':');
				if (num < 0)
				{
					return value;
				}
				return value.Substring(num + 1);
			}

			// Token: 0x040008E2 RID: 2274
			private const string XsString = "string";

			// Token: 0x040008E3 RID: 2275
			private const string XsInt = "int";

			// Token: 0x040008E4 RID: 2276
			private const string XsInteger = "integer";

			// Token: 0x040008E5 RID: 2277
			private const string XsLong = "long";

			// Token: 0x040008E6 RID: 2278
			private const string XsBoolean = "boolean";

			// Token: 0x040008E7 RID: 2279
			private static readonly XNamespace XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

			// Token: 0x040008E8 RID: 2280
			private static readonly XName XsiTypeAttr = LinguisticSchemaUpgrader.V001ToV002Upgrader.XsiNamespace + "type";

			// Token: 0x040008E9 RID: 2281
			private static readonly XName EdmElementElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "EdmElement";

			// Token: 0x040008EA RID: 2282
			private static readonly XName EdmElementsElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "EdmElements";

			// Token: 0x040008EB RID: 2283
			private static readonly XName ValueElem = LinguisticSchemaUpgrader.SchemaConstants.Namespace + "Value";

			// Token: 0x040008EC RID: 2284
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;

			// Token: 0x040008ED RID: 2285
			private readonly Dictionary<LinguisticSchemaUpgrader.EdmElementKey, string> _tableNames;

			// Token: 0x040008EE RID: 2286
			private readonly HashSet<string> _entityNames;

			// Token: 0x040008EF RID: 2287
			private Dictionary<LinguisticSchemaUpgrader.EdmElementKey, XElement> _entitiesByEdm;

			// Token: 0x040008F0 RID: 2288
			private LanguageIdentifier _language;
		}

		// Token: 0x02000222 RID: 546
		private sealed class V002ToV003Upgrader
		{
			// Token: 0x06000BAF RID: 2991 RVA: 0x00017040 File Offset: 0x00015240
			private V002ToV003Upgrader(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				this._context = context;
			}

			// Token: 0x06000BB0 RID: 2992 RVA: 0x0001704F File Offset: 0x0001524F
			internal static void Upgrade(LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext context)
			{
				new LinguisticSchemaUpgrader.V002ToV003Upgrader(context).Upgrade();
			}

			// Token: 0x06000BB1 RID: 2993 RVA: 0x0001705C File Offset: 0x0001525C
			private void Upgrade()
			{
				XAttribute xattribute = this._context.Schema.Root.Attribute(LinguisticSchemaUpgrader.SchemaConstants.DynamicImprovementAttr);
				ModelDynamicImprovement modelDynamicImprovement;
				if (xattribute == null || !Enum.TryParse<ModelDynamicImprovement>(xattribute.Value, out modelDynamicImprovement))
				{
					modelDynamicImprovement = ModelDynamicImprovement.Full;
				}
				XElement xelement = this._context.Schema.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.EntitiesElem);
				if (xelement != null)
				{
					LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.MergeDuplicateEntities(xelement, this._context);
					foreach (XElement xelement2 in xelement.Elements(LinguisticSchemaUpgrader.SchemaConstants.EntityElem))
					{
						if (modelDynamicImprovement != ModelDynamicImprovement.None)
						{
							LinguisticSchemaUpgrader.LinguisticSchemaUpgradeUtil.UpgradeEntitySource(xelement2);
						}
						this.UpgradeEntityEdmReferences(xelement2);
					}
				}
				XElement xelement3 = this._context.Schema.Root.Element(LinguisticSchemaUpgrader.SchemaConstants.RelationshipsElem);
				if (xelement3 != null)
				{
					foreach (XElement xelement4 in xelement3.Elements(LinguisticSchemaUpgrader.SchemaConstants.RelationshipElem))
					{
						this.UpgradeRelationshipEdmReferences(xelement4);
					}
				}
			}

			// Token: 0x06000BB2 RID: 2994 RVA: 0x00017180 File Offset: 0x00015380
			private void UpgradeEntityEdmReferences(XElement entity)
			{
				IConceptualEntity conceptualEntity;
				if (this.TryUpgradeEdmEntityReference(entity, out conceptualEntity))
				{
					this.UpgradeEdmPropertyReference(entity, LinguisticSchemaUpgrader.SchemaConstants.EdmPropertyAttr, LinguisticSchemaUpgrader.SchemaConstants.ConceptualPropertyAttr, conceptualEntity);
					XElement xelement = entity.Element(LinguisticSchemaUpgrader.SchemaConstants.InstanceWeightsElem);
					if (xelement != null)
					{
						this.UpgradeEdmPropertyReference(xelement, LinguisticSchemaUpgrader.SchemaConstants.EdmPropertyAttr, LinguisticSchemaUpgrader.SchemaConstants.ConceptualPropertyAttr, conceptualEntity);
					}
				}
				XElement xelement2 = entity.Element(LinguisticSchemaUpgrader.SchemaConstants.InstanceSynonymsElem);
				IConceptualEntity conceptualEntity2;
				if (xelement2 != null && this.TryUpgradeEdmEntityReference(xelement2, out conceptualEntity2))
				{
					this.UpgradeEdmPropertyReference(xelement2, LinguisticSchemaUpgrader.SchemaConstants.WordEdmPropertyAttr, LinguisticSchemaUpgrader.SchemaConstants.WordConceptualPropertyAttr, conceptualEntity2);
					this.UpgradeEdmPropertyReference(xelement2, LinguisticSchemaUpgrader.SchemaConstants.ValueEdmPropertyAttr, LinguisticSchemaUpgrader.SchemaConstants.ValueConceptualPropertyAttr, conceptualEntity2);
				}
			}

			// Token: 0x06000BB3 RID: 2995 RVA: 0x0001720C File Offset: 0x0001540C
			private void UpgradeRelationshipEdmReferences(XElement relationship)
			{
				IConceptualEntity conceptualEntity;
				this.TryUpgradeEdmEntityReference(relationship, out conceptualEntity);
				foreach (XElement xelement in from roles in relationship.Elements(LinguisticSchemaUpgrader.SchemaConstants.RolesElem)
					from role in roles.Elements(LinguisticSchemaUpgrader.SchemaConstants.RoleElem)
					from path in role.Elements(LinguisticSchemaUpgrader.SchemaConstants.PathElem)
					from segment in path.Elements(LinguisticSchemaUpgrader.SchemaConstants.SegmentElem)
					select segment)
				{
					string attribute = xelement.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.EdmNavigationPropertyAttr);
					xelement.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.ConceptualNavigationPropertyAttr, attribute);
					xelement.RemoveAttributes(new XName[] { LinguisticSchemaUpgrader.SchemaConstants.EdmNavigationPropertyAttr });
				}
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x00017354 File Offset: 0x00015554
			private bool TryUpgradeEdmEntityReference(XElement element, out IConceptualEntity conceptualEntity)
			{
				string attribute = element.GetAttribute(LinguisticSchemaUpgrader.SchemaConstants.EdmEntitySetAttr);
				if (!string.IsNullOrEmpty(attribute))
				{
					string text;
					if (this._context.ConceptualSchema != null && this._context.ConceptualSchema.TryGetEntityByEdmName(attribute, out conceptualEntity))
					{
						text = conceptualEntity.Name;
					}
					else
					{
						text = LinguisticEntityNameUtil.RemoveEntityContainer(attribute);
						conceptualEntity = null;
					}
					element.SetAttributeValue(LinguisticSchemaUpgrader.SchemaConstants.ConceptualEntityAttr, text);
					element.RemoveAttributes(new XName[] { LinguisticSchemaUpgrader.SchemaConstants.EdmEntitySetAttr });
					return true;
				}
				conceptualEntity = null;
				return false;
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x000173D0 File Offset: 0x000155D0
			private void UpgradeEdmPropertyReference(XElement element, XName edmPropAttr, XName conceptualPropAttr, IConceptualEntity conceptualEntity)
			{
				string attribute = element.GetAttribute(edmPropAttr);
				if (!string.IsNullOrEmpty(attribute))
				{
					IConceptualProperty conceptualProperty;
					string text;
					if (conceptualEntity != null && conceptualEntity.TryGetPropertyByEdmName(attribute, out conceptualProperty))
					{
						text = conceptualProperty.Name;
					}
					else
					{
						text = attribute;
					}
					element.SetAttributeValue(conceptualPropAttr, text);
					element.RemoveAttributes(new XName[] { edmPropAttr });
				}
			}

			// Token: 0x040008F1 RID: 2289
			private readonly LinguisticSchemaUpgrader.LinguisticSchemaUpgradeContext _context;
		}
	}
}
