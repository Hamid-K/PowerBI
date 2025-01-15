using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.Lucia.Json;
using Microsoft.Lucia.Yaml;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using YamlDotNet.Serialization.TypeInspectors;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001DD RID: 477
	public static class LsdlYamlSerializer
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00013421 File Offset: 0x00011621
		public static LsdlDocument ReadYaml(TextReader reader)
		{
			return LsdlYamlSerializer.ReadYaml<LsdlDocument>(reader);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00013429 File Offset: 0x00011629
		internal static T ReadYaml<T>(TextReader reader)
		{
			return LsdlJsonSerializer.ReadJson<T>(JsonReaderFactory.CreateFromYaml(reader, false), false);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00013438 File Offset: 0x00011638
		public static LsdlDocument FromYamlString(string content)
		{
			return LsdlYamlSerializer.ReadYaml(new StringReader(content));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00013448 File Offset: 0x00011648
		public static void WriteYaml(this LsdlDocument lsdlDocument, TextWriter writer, LsdlSerializerSettings settings = null)
		{
			LsdlYamlSerializer.CreateSerializerBuilder(settings ?? LsdlSerializerSettings.Default).Build().Serialize(new TextWriterEmitter(writer), lsdlDocument);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00013478 File Offset: 0x00011678
		public static string ToYamlString(this LsdlDocument lsdlDocument, LsdlSerializerSettings settings = null)
		{
			string text;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				lsdlDocument.WriteYaml(stringWriter, settings);
				text = stringWriter.GetStringBuilder().TrimEnd().ToString();
			}
			return text;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000134C8 File Offset: 0x000116C8
		public static SerializerBuilder WithDefaultPropertiesOmitted(this SerializerBuilder builder)
		{
			return builder.WithEmissionPhaseObjectGraphVisitor<LsdlYamlSerializer.OmitDefaultPropertyObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new LsdlYamlSerializer.OmitDefaultPropertyObjectGraphVisitor(a));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000134F0 File Offset: 0x000116F0
		internal static void WriteYaml(object obj, TextWriter writer, LsdlSerializerSettings settings = null)
		{
			LsdlYamlSerializer.CreateSerializerBuilder(settings ?? LsdlSerializerSettings.Default).Build().Serialize(new TextWriterEmitter(writer), obj);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00013520 File Offset: 0x00011720
		internal static void WriteNestedYaml(this LsdlDocument lsdlDocument, IEmitter emitter, LsdlSerializerSettings settings = null)
		{
			LsdlYamlSerializer.CreateSerializerBuilder(settings ?? LsdlSerializerSettings.Default).BuildValueSerializer().SerializeValue(emitter, lsdlDocument, typeof(LsdlDocument));
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00013554 File Offset: 0x00011754
		private static SerializerBuilder CreateSerializerBuilder(ILsdlSerializerSettings settings)
		{
			SerializerBuilder serializerBuilder = new SerializerBuilder().DisableAliases().WithJsonAttributeHandling().WithJsonStyleScalars()
				.WithEmptyCollectionsOmitted()
				.WithUnionSerialization()
				.WithCustomSerialization(null)
				.WithDefaultPropertiesOmitted()
				.ConfigureDefaultValuesHandling(2)
				.WithTypeConverter(new VersionTypeConverter());
			if (!settings.CanonicalBindings)
			{
				serializerBuilder = serializerBuilder.WithTypeInspector<LsdlYamlSerializer.CommonBindingNamesRewriter>(([Nullable(1)] ITypeInspector i) => new LsdlYamlSerializer.CommonBindingNamesRewriter(i, settings.ConceptualSchema));
			}
			if (!settings.CanonicalForm)
			{
				serializerBuilder = serializerBuilder.WithEmissionPhaseObjectGraphVisitor<LsdlYamlSerializer.ScalarFormObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new LsdlYamlSerializer.ScalarFormObjectGraphVisitor(a)).WithEmissionPhaseObjectGraphVisitor<LsdlYamlSerializer.RemoveTriviallyObviousGeneratedContentObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new LsdlYamlSerializer.RemoveTriviallyObviousGeneratedContentObjectGraphVisitor(a)).WithEmissionPhaseObjectGraphVisitor<LsdlYamlSerializer.RelationshipRoleObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new LsdlYamlSerializer.RelationshipRoleObjectGraphVisitor(a));
			}
			if (settings.OmitVersion)
			{
				serializerBuilder = serializerBuilder.WithTypeInspector<LsdlYamlSerializer.OmitVersionTypeInspector>(([Nullable(1)] ITypeInspector i) => new LsdlYamlSerializer.OmitVersionTypeInspector(i));
			}
			return serializerBuilder;
		}

		// Token: 0x02000249 RID: 585
		private sealed class ScalarFormObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000CA4 RID: 3236 RVA: 0x0001A3FB File Offset: 0x000185FB
			public ScalarFormObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
				this._nestedObjectSerializer = args.NestedObjectSerializer;
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x0001A418 File Offset: 0x00018618
			public override bool Enter(IObjectDescriptor value, IEmitter emitter)
			{
				IScalarForm<string> scalarForm = value.Value as IScalarForm<string>;
				string text;
				if (scalarForm != null && scalarForm.TryGetScalarForm(out text))
				{
					this._nestedObjectSerializer.Invoke(text, null);
					return false;
				}
				IScalarForm<Union<long, double>> scalarForm2 = value.Value as IScalarForm<Union<long, double>>;
				Union<long, double> union;
				if (scalarForm2 != null && scalarForm2.TryGetScalarForm(out union))
				{
					this._nestedObjectSerializer.Invoke((union != null) ? union.AsObject() : null, null);
					return false;
				}
				IScalarForm<bool?> scalarForm3 = value.Value as IScalarForm<bool?>;
				bool? flag;
				if (scalarForm3 != null && scalarForm3.TryGetScalarForm(out flag))
				{
					this._nestedObjectSerializer.Invoke(flag, null);
					return false;
				}
				IScalarForm<IValueList> scalarForm4 = value.Value as IScalarForm<IValueList>;
				IValueList valueList;
				if (scalarForm4 != null && scalarForm4.TryGetScalarForm(out valueList))
				{
					this._nestedObjectSerializer.Invoke(valueList, null);
					return false;
				}
				return base.Enter(value, emitter);
			}

			// Token: 0x04000973 RID: 2419
			private readonly ObjectSerializer _nestedObjectSerializer;
		}

		// Token: 0x0200024A RID: 586
		private sealed class RemoveTriviallyObviousGeneratedContentObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000CA6 RID: 3238 RVA: 0x0001A4E5 File Offset: 0x000186E5
			internal RemoveTriviallyObviousGeneratedContentObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000CA7 RID: 3239 RVA: 0x0001A504 File Offset: 0x00018704
			public override void VisitSequenceStart(IObjectDescriptor sequence, Type elementType, IEmitter context)
			{
				TermList termList = sequence.Value as TermList;
				if (termList != null)
				{
					this._termsToSerialize.Clear();
					this._termsToSerialize.UnionWith(termList.GetItemsToSerialize());
				}
				base.VisitSequenceStart(sequence, elementType, context);
			}

			// Token: 0x06000CA8 RID: 3240 RVA: 0x0001A548 File Offset: 0x00018748
			public override bool Enter(IObjectDescriptor value, IEmitter context)
			{
				Term term = value.Value as Term;
				return (term == null || this._termsToSerialize.Contains(term)) && base.Enter(value, context);
			}

			// Token: 0x04000974 RID: 2420
			private readonly HashSet<Term> _termsToSerialize = new HashSet<Term>(ReferenceEqualityComparer<Term>.Instance);
		}

		// Token: 0x0200024B RID: 587
		private sealed class RelationshipRoleObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000CA9 RID: 3241 RVA: 0x0001A57C File Offset: 0x0001877C
			internal RelationshipRoleObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000CAA RID: 3242 RVA: 0x0001A58C File Offset: 0x0001878C
			public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
			{
				IReadOnlyDictionary<string, Role> readOnlyDictionary = value.Value as IReadOnlyDictionary<string, Role>;
				return (readOnlyDictionary == null || !readOnlyDictionary.All(new Func<KeyValuePair<string, Role>, bool>(this.ShouldSkipRole))) && base.EnterMapping(key, value, context);
			}

			// Token: 0x06000CAB RID: 3243 RVA: 0x0001A5C8 File Offset: 0x000187C8
			public override bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value, IEmitter context)
			{
				Relationship relationship = value.Value as Relationship;
				if (relationship != null)
				{
					this._implicitRoles = relationship.GetAllImplicitRoles();
				}
				string text = key.Value as string;
				if (text != null)
				{
					Role role = value.Value as Role;
					if (role != null && this.ShouldSkipRole(text.WithValue(role)))
					{
						return false;
					}
				}
				return base.EnterMapping(key, value, context);
			}

			// Token: 0x06000CAC RID: 3244 RVA: 0x0001A628 File Offset: 0x00018828
			private bool ShouldSkipRole(KeyValuePair<string, Role> role)
			{
				string text;
				return role.Value.TryGetScalarForm(out text) && role.Key == text && this._implicitRoles.Contains(role.Key);
			}

			// Token: 0x04000975 RID: 2421
			private HashSet<string> _implicitRoles;
		}

		// Token: 0x0200024C RID: 588
		private sealed class CommonBindingNamesRewriter : TypeInspectorSkeleton
		{
			// Token: 0x06000CAD RID: 3245 RVA: 0x0001A668 File Offset: 0x00018868
			internal CommonBindingNamesRewriter(ITypeInspector innerTypeInspector, IConceptualSchema conceptualSchema)
			{
				this._innerTypeInspector = innerTypeInspector;
				this._conceptualSchema = conceptualSchema;
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x0001A67E File Offset: 0x0001887E
			public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
			{
				bool mapBindingPropertiesForType = typeof(Binding).IsAssignableFrom(type);
				foreach (IPropertyDescriptor propertyDescriptor in this._innerTypeInspector.GetProperties(type, container))
				{
					if (mapBindingPropertiesForType && this.ShouldMapBindingProperties(container))
					{
						string text = null;
						string name = propertyDescriptor.Name;
						if (!(name == "ConceptualEntity"))
						{
							if (name == "ConceptualProperty")
							{
								text = (this.IsMeasureBinding(container) ? "Measure" : "Column");
							}
						}
						else
						{
							text = "Table";
						}
						if (text != null)
						{
							yield return new PropertyDescriptor(propertyDescriptor)
							{
								Name = text
							};
							continue;
						}
					}
					yield return propertyDescriptor;
				}
				IEnumerator<IPropertyDescriptor> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x0001A69C File Offset: 0x0001889C
			private bool ShouldMapBindingProperties(object container)
			{
				if (this._conceptualSchema != null)
				{
					IConceptualEntityBinding conceptualEntityBinding = container as IConceptualEntityBinding;
					IConceptualEntity conceptualEntity;
					if (conceptualEntityBinding != null && this._conceptualSchema.TryGetEntity(conceptualEntityBinding.ConceptualEntity, out conceptualEntity))
					{
						return !(conceptualEntity is IConceptualPod);
					}
				}
				return true;
			}

			// Token: 0x06000CB0 RID: 3248 RVA: 0x0001A6E0 File Offset: 0x000188E0
			private bool IsMeasureBinding(object container)
			{
				if (this._conceptualSchema != null)
				{
					ConceptualPropertyBinding conceptualPropertyBinding = container as ConceptualPropertyBinding;
					IConceptualEntity conceptualEntity;
					IConceptualProperty conceptualProperty;
					if (conceptualPropertyBinding != null && conceptualPropertyBinding.VariationSource == null && conceptualPropertyBinding.VariationSet == null && this._conceptualSchema.TryGetEntity(conceptualPropertyBinding.ConceptualEntity, out conceptualEntity) && conceptualEntity.TryGetProperty(conceptualPropertyBinding.ConceptualProperty, out conceptualProperty))
					{
						return conceptualProperty is IConceptualMeasure;
					}
				}
				return false;
			}

			// Token: 0x04000976 RID: 2422
			private readonly ITypeInspector _innerTypeInspector;

			// Token: 0x04000977 RID: 2423
			private readonly IConceptualSchema _conceptualSchema;
		}

		// Token: 0x0200024D RID: 589
		private sealed class OmitVersionTypeInspector : TypeInspectorSkeleton
		{
			// Token: 0x06000CB1 RID: 3249 RVA: 0x0001A73F File Offset: 0x0001893F
			internal OmitVersionTypeInspector(ITypeInspector innerTypeInspector)
			{
				this._innerTypeInspector = innerTypeInspector;
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x0001A74E File Offset: 0x0001894E
			public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
			{
				bool omitVersionPropertyForType = typeof(LsdlDocument).IsAssignableFrom(type);
				foreach (IPropertyDescriptor propertyDescriptor in this._innerTypeInspector.GetProperties(type, container))
				{
					if (!omitVersionPropertyForType || !(propertyDescriptor.Name == "Version"))
					{
						yield return propertyDescriptor;
					}
				}
				IEnumerator<IPropertyDescriptor> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04000978 RID: 2424
			private readonly ITypeInspector _innerTypeInspector;
		}

		// Token: 0x0200024E RID: 590
		private sealed class OmitDefaultPropertyObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000CB3 RID: 3251 RVA: 0x0001A76C File Offset: 0x0001896C
			internal OmitDefaultPropertyObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x0001A77C File Offset: 0x0001897C
			public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter emitter)
			{
				object obj = value.Value;
				if (obj is EnumProperty<EntityVisibility>)
				{
					if (!((EnumProperty<EntityVisibility>)obj).ShouldSerialize())
					{
						return false;
					}
				}
				else
				{
					obj = value.Value;
					if (obj is Source && !((Source)obj).ShouldSerialize())
					{
						return false;
					}
				}
				return base.EnterMapping(key, value, emitter);
			}
		}
	}
}
