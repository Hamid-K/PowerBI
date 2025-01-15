using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.InfoNav;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.ObjectGraphTraversalStrategies;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using YamlDotNet.Serialization.TypeInspectors;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x02000022 RID: 34
	public static class YamlSerializationExtensions
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003130 File Offset: 0x00001330
		public static SerializerBuilder WithJsonAttributeHandling(this SerializerBuilder builder)
		{
			return builder.WithEmissionPhaseObjectGraphVisitor<YamlSerializationExtensions.JsonAttributesObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new YamlSerializationExtensions.JsonAttributesObjectGraphVisitor(a)).WithTypeInspector<YamlSerializationExtensions.JsonAttributesTypeInspector>(([Nullable(1)] ITypeInspector i) => new YamlSerializationExtensions.JsonAttributesTypeInspector(i));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003188 File Offset: 0x00001388
		public static SerializerBuilder WithDataContractAttributeHandling(this SerializerBuilder builder)
		{
			return builder.WithEmissionPhaseObjectGraphVisitor<YamlSerializationExtensions.DataContractAttributesObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new YamlSerializationExtensions.DataContractAttributesObjectGraphVisitor(a)).WithTypeInspector<YamlSerializationExtensions.DataContractAttributesTypeInspector>(([Nullable(1)] ITypeInspector i) => new YamlSerializationExtensions.DataContractAttributesTypeInspector(i));
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000031E0 File Offset: 0x000013E0
		public static SerializerBuilder WithJsonStyleScalars(this SerializerBuilder builder)
		{
			return builder.WithEventEmitter<YamlSerializationExtensions.JsonStyleScalarEventEmitter>(([Nullable(1)] IEventEmitter e) => new YamlSerializationExtensions.JsonStyleScalarEventEmitter(e)).WithTypeConverter(new DateTimeConverter(DateTimeKind.Utc, null, new string[] { "yyyy-MM-ddTHH:mm:ss.FFFFFFFK" }));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000322C File Offset: 0x0000142C
		public static SerializerBuilder WithBasePropertiesFirstOrdering(this SerializerBuilder builder)
		{
			return builder.WithTypeInspector<YamlSerializationExtensions.BasePropertiesFirstOrderingTypeInspector>(([Nullable(1)] ITypeInspector i) => new YamlSerializationExtensions.BasePropertiesFirstOrderingTypeInspector(i));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003253 File Offset: 0x00001453
		public static SerializerBuilder WithEmptyCollectionsOmitted(this SerializerBuilder builder)
		{
			return builder.WithEmissionPhaseObjectGraphVisitor<YamlSerializationExtensions.OmitEmptyCollectionsObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new YamlSerializationExtensions.OmitEmptyCollectionsObjectGraphVisitor(a));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000327A File Offset: 0x0000147A
		public static SerializerBuilder WithUnionSerialization(this SerializerBuilder builder)
		{
			return builder.WithEmissionPhaseObjectGraphVisitor<YamlSerializationExtensions.UnionObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new YamlSerializationExtensions.UnionObjectGraphVisitor(a));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000032A4 File Offset: 0x000014A4
		public static SerializerBuilder WithSelectedObjectsOmitted(this SerializerBuilder builder, ISet<string> objects)
		{
			if (!objects.IsNullOrEmptyCollection<string>())
			{
				return builder.WithEmissionPhaseObjectGraphVisitor<YamlSerializationExtensions.OmitSelectedObjectsObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new YamlSerializationExtensions.OmitSelectedObjectsObjectGraphVisitor(a, objects));
			}
			return builder;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000032E0 File Offset: 0x000014E0
		public static SerializerBuilder WithCustomSerialization(this SerializerBuilder builder, Func<object, Type, YamlSerializationOptions?> overrideOptionsForObject = null)
		{
			return builder.WithEventEmitter<YamlSerializationExtensions.CustomSerializationEventEmitter>(([Nullable(1)] IEventEmitter e) => new YamlSerializationExtensions.CustomSerializationEventEmitter(e, overrideOptionsForObject));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000330C File Offset: 0x0000150C
		public static SerializerBuilder WithIgnoreCyclicReferenceStrategy(this SerializerBuilder builder)
		{
			return builder.WithObjectGraphTraversalStrategyFactory((ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters, int maximumRecursion) => new YamlSerializationExtensions.IgnoreCyclicReferenceObjectGraphTraversalStrategy(typeInspector, typeResolver, maximumRecursion, NullNamingConvention.Instance));
		}

		// Token: 0x020001E7 RID: 487
		private sealed class DataContractAttributesObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000A9E RID: 2718 RVA: 0x00013A74 File Offset: 0x00011C74
			internal DataContractAttributesObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000A9F RID: 2719 RVA: 0x00013A84 File Offset: 0x00011C84
			public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter emitter)
			{
				DataMemberAttribute customAttribute = key.GetCustomAttribute<DataMemberAttribute>();
				return (customAttribute != null && customAttribute.EmitDefaultValue) || base.EnterMapping(key, value, emitter);
			}
		}

		// Token: 0x020001E8 RID: 488
		private sealed class DataContractAttributesTypeInspector : TypeInspectorSkeleton
		{
			// Token: 0x06000AA0 RID: 2720 RVA: 0x00013AAE File Offset: 0x00011CAE
			internal DataContractAttributesTypeInspector(ITypeInspector innerTypeInspector)
			{
				this._innerTypeInspector = innerTypeInspector;
			}

			// Token: 0x06000AA1 RID: 2721 RVA: 0x00013AC0 File Offset: 0x00011CC0
			public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
			{
				IEnumerable<IPropertyDescriptor> properties = this._innerTypeInspector.GetProperties(type, container);
				if (type.GetCustomAttributes(typeof(DataContractAttribute), true).IsNullOrEmpty<object>())
				{
					return properties;
				}
				return properties.Where((IPropertyDescriptor p) => p.GetCustomAttribute<DataMemberAttribute>() != null && p.GetCustomAttribute<IgnoreDataMemberAttribute>() == null);
			}

			// Token: 0x04000801 RID: 2049
			private readonly ITypeInspector _innerTypeInspector;
		}

		// Token: 0x020001E9 RID: 489
		private sealed class JsonAttributesObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000AA2 RID: 2722 RVA: 0x00013B1A File Offset: 0x00011D1A
			internal JsonAttributesObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x00013B28 File Offset: 0x00011D28
			public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter emitter)
			{
				JsonPropertyAttribute customAttribute = key.GetCustomAttribute<JsonPropertyAttribute>();
				return (customAttribute != null && (customAttribute.DefaultValueHandling == DefaultValueHandling.Include || customAttribute.DefaultValueHandling == DefaultValueHandling.Populate)) || base.EnterMapping(key, value, emitter);
			}
		}

		// Token: 0x020001EA RID: 490
		private sealed class JsonAttributesTypeInspector : TypeInspectorSkeleton
		{
			// Token: 0x06000AA4 RID: 2724 RVA: 0x00013B5B File Offset: 0x00011D5B
			internal JsonAttributesTypeInspector(ITypeInspector innerTypeInspector)
			{
				this._innerTypeInspector = innerTypeInspector;
			}

			// Token: 0x06000AA5 RID: 2725 RVA: 0x00013B6C File Offset: 0x00011D6C
			public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
			{
				return (from p in this._innerTypeInspector.GetProperties(type, container)
					where p.GetCustomAttribute<JsonIgnoreAttribute>() == null
					select p).Select(delegate(IPropertyDescriptor p)
				{
					JsonPropertyAttribute customAttribute = p.GetCustomAttribute<JsonPropertyAttribute>();
					if (customAttribute == null || customAttribute.PropertyName == null)
					{
						return p;
					}
					return new PropertyDescriptor(p)
					{
						Name = customAttribute.PropertyName
					};
				});
			}

			// Token: 0x04000802 RID: 2050
			private readonly ITypeInspector _innerTypeInspector;
		}

		// Token: 0x020001EB RID: 491
		private sealed class JsonStyleScalarEventEmitter : ChainedEventEmitter
		{
			// Token: 0x06000AA6 RID: 2726 RVA: 0x00013BCE File Offset: 0x00011DCE
			internal JsonStyleScalarEventEmitter(IEventEmitter next)
				: base(next)
			{
			}

			// Token: 0x06000AA7 RID: 2727 RVA: 0x00013BD8 File Offset: 0x00011DD8
			public override void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
			{
				if (eventInfo.Source.Value == null)
				{
					emitter.EmitNull();
					return;
				}
				object obj = eventInfo.Source.Value;
				if (obj is double)
				{
					double num = (double)obj;
					emitter.Emit(num);
					return;
				}
				obj = eventInfo.Source.Value;
				if (obj is float)
				{
					float num2 = (float)obj;
					emitter.Emit(num2);
					return;
				}
				base.Emit(eventInfo, emitter);
			}
		}

		// Token: 0x020001EC RID: 492
		private sealed class BasePropertiesFirstOrderingTypeInspector : TypeInspectorSkeleton
		{
			// Token: 0x06000AA8 RID: 2728 RVA: 0x00013C47 File Offset: 0x00011E47
			internal BasePropertiesFirstOrderingTypeInspector(ITypeInspector innerTypeInspector)
			{
				this._innerTypeInspector = innerTypeInspector;
			}

			// Token: 0x06000AA9 RID: 2729 RVA: 0x00013C56 File Offset: 0x00011E56
			public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container)
			{
				IReadOnlyList<IPropertyDescriptor> props = this._innerTypeInspector.GetProperties(type, container).AsReadOnlyList<IPropertyDescriptor>();
				Stack<IPropertyDescriptor> typeProps = new Stack<IPropertyDescriptor>();
				Type type2 = null;
				int num;
				for (int i = props.Count - 1; i >= 0; i = num - 1)
				{
					IPropertyDescriptor prop = props[i];
					PropertyInfo property = type.GetProperty(prop.Name);
					Type propType = ((property != null) ? property.DeclaringType : null);
					if (propType != type2)
					{
						while (typeProps.Count > 0)
						{
							yield return typeProps.Pop();
						}
						typeProps.Clear();
						type2 = propType;
					}
					typeProps.Push(prop);
					prop = null;
					propType = null;
					num = i;
				}
				while (typeProps.Count > 0)
				{
					yield return typeProps.Pop();
				}
				yield break;
			}

			// Token: 0x04000803 RID: 2051
			private readonly ITypeInspector _innerTypeInspector;
		}

		// Token: 0x020001ED RID: 493
		private sealed class OmitEmptyCollectionsObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000AAA RID: 2730 RVA: 0x00013C74 File Offset: 0x00011E74
			internal OmitEmptyCollectionsObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000AAB RID: 2731 RVA: 0x00013C84 File Offset: 0x00011E84
			public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter emitter)
			{
				if (key.Type != typeof(string))
				{
					if (typeof(ICollection).IsAssignableFrom(key.Type))
					{
						ICollection collection = value.Value as ICollection;
						if (collection == null || collection.Count == 0)
						{
							return false;
						}
					}
					else if (typeof(IEnumerable).IsAssignableFrom(key.Type))
					{
						IEnumerable enumerable = value.Value as IEnumerable;
						if (enumerable == null || !enumerable.GetEnumerator().MoveNext())
						{
							return false;
						}
					}
				}
				return base.EnterMapping(key, value, emitter);
			}
		}

		// Token: 0x020001EE RID: 494
		private sealed class UnionObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000AAC RID: 2732 RVA: 0x00013D16 File Offset: 0x00011F16
			internal UnionObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
				this._nestedObjectSerializer = args.NestedObjectSerializer;
			}

			// Token: 0x06000AAD RID: 2733 RVA: 0x00013D30 File Offset: 0x00011F30
			public override bool Enter(IObjectDescriptor value, IEmitter emitter)
			{
				UnionBase unionBase = value.Value as UnionBase;
				if (unionBase != null)
				{
					this._nestedObjectSerializer.Invoke(unionBase.AsObject(), null);
					return false;
				}
				return base.Enter(value, emitter);
			}

			// Token: 0x04000804 RID: 2052
			private readonly ObjectSerializer _nestedObjectSerializer;
		}

		// Token: 0x020001EF RID: 495
		private sealed class OmitSelectedObjectsObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000AAE RID: 2734 RVA: 0x00013D68 File Offset: 0x00011F68
			internal OmitSelectedObjectsObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args, ISet<string> objects)
				: base(args.InnerVisitor)
			{
				this._objects = objects;
			}

			// Token: 0x06000AAF RID: 2735 RVA: 0x00013D7D File Offset: 0x00011F7D
			public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter emitter)
			{
				return !this._objects.Contains(key.Name) && base.EnterMapping(key, value, emitter);
			}

			// Token: 0x04000805 RID: 2053
			private readonly ISet<string> _objects;
		}

		// Token: 0x020001F0 RID: 496
		private sealed class CustomSerializationEventEmitter : ChainedEventEmitter
		{
			// Token: 0x06000AB0 RID: 2736 RVA: 0x00013D9D File Offset: 0x00011F9D
			internal CustomSerializationEventEmitter(IEventEmitter next, Func<object, Type, YamlSerializationOptions?> overrideOptionsForObject)
				: base(next)
			{
				this._overrideOptionsForObject = overrideOptionsForObject;
				this._parentEventInfo = ((overrideOptionsForObject != null) ? new Stack<Type>() : null);
			}

			// Token: 0x06000AB1 RID: 2737 RVA: 0x00013DBE File Offset: 0x00011FBE
			public override void Emit(SequenceStartEventInfo eventInfo, IEmitter emitter)
			{
				if (this.GetOptionsForObject(eventInfo.Source.Value).IsCompact())
				{
					eventInfo.Style = 2;
				}
				Stack<Type> parentEventInfo = this._parentEventInfo;
				if (parentEventInfo != null)
				{
					parentEventInfo.Push(eventInfo.GetType());
				}
				base.Emit(eventInfo, emitter);
			}

			// Token: 0x06000AB2 RID: 2738 RVA: 0x00013DFE File Offset: 0x00011FFE
			public override void Emit(MappingStartEventInfo eventInfo, IEmitter emitter)
			{
				if (this.GetOptionsForObject(eventInfo.Source.Value).IsCompact())
				{
					eventInfo.Style = 2;
				}
				Stack<Type> parentEventInfo = this._parentEventInfo;
				if (parentEventInfo != null)
				{
					parentEventInfo.Push(eventInfo.GetType());
				}
				base.Emit(eventInfo, emitter);
			}

			// Token: 0x06000AB3 RID: 2739 RVA: 0x00013E40 File Offset: 0x00012040
			public override void Emit(SequenceEndEventInfo eventInfo, IEmitter emitter)
			{
				base.Emit(eventInfo, emitter);
				Stack<Type> parentEventInfo = this._parentEventInfo;
				if (parentEventInfo != null)
				{
					parentEventInfo.Pop();
				}
				if (this.GetOptionsForObject(eventInfo.Source.Value).IsLineBreakAfter())
				{
					ITextWriterEmitter textWriterEmitter = emitter as ITextWriterEmitter;
					if (textWriterEmitter != null)
					{
						textWriterEmitter.Output.WriteLine();
					}
				}
			}

			// Token: 0x06000AB4 RID: 2740 RVA: 0x00013E94 File Offset: 0x00012094
			public override void Emit(MappingEndEventInfo eventInfo, IEmitter emitter)
			{
				base.Emit(eventInfo, emitter);
				Stack<Type> parentEventInfo = this._parentEventInfo;
				if (parentEventInfo != null)
				{
					parentEventInfo.Pop();
				}
				if (this.GetOptionsForObject(eventInfo.Source.Value).IsLineBreakAfter())
				{
					ITextWriterEmitter textWriterEmitter = emitter as ITextWriterEmitter;
					if (textWriterEmitter != null)
					{
						textWriterEmitter.Output.WriteLine();
					}
				}
			}

			// Token: 0x06000AB5 RID: 2741 RVA: 0x00013EE8 File Offset: 0x000120E8
			private YamlSerializationOptions GetOptionsForObject(object o)
			{
				if (this._overrideOptionsForObject != null)
				{
					Type type = ((this._parentEventInfo.Count > 0) ? this._parentEventInfo.Peek() : null);
					YamlSerializationOptions? yamlSerializationOptions = this._overrideOptionsForObject(o, type);
					if (yamlSerializationOptions != null)
					{
						return yamlSerializationOptions.Value;
					}
				}
				ICustomSerializationOptions customSerializationOptions = o as ICustomSerializationOptions;
				if (customSerializationOptions != null)
				{
					return customSerializationOptions.Options;
				}
				return YamlSerializationOptions.None;
			}

			// Token: 0x04000806 RID: 2054
			private readonly Func<object, Type, YamlSerializationOptions?> _overrideOptionsForObject;

			// Token: 0x04000807 RID: 2055
			[Nullable]
			private readonly Stack<Type> _parentEventInfo;
		}

		// Token: 0x020001F1 RID: 497
		private sealed class IgnoreCyclicReferenceObjectGraphTraversalStrategy : FullObjectGraphTraversalStrategy
		{
			// Token: 0x06000AB6 RID: 2742 RVA: 0x00013F4B File Offset: 0x0001214B
			internal IgnoreCyclicReferenceObjectGraphTraversalStrategy(ITypeInspector typeDescriptor, ITypeResolver typeResolver, int maxRecursion, INamingConvention namingConvention)
				: base(typeDescriptor, typeResolver, maxRecursion, namingConvention)
			{
			}

			// Token: 0x06000AB7 RID: 2743 RVA: 0x00013F58 File Offset: 0x00012158
			protected override void Traverse<[Nullable(2)] TContext>(object name, IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<FullObjectGraphTraversalStrategy.ObjectPathSegment> path)
			{
				if (path.Any((FullObjectGraphTraversalStrategy.ObjectPathSegment e) => e.value.Value == value.Value))
				{
					visitor.VisitScalar(YamlSerializationExtensions.IgnoreCyclicReferenceObjectGraphTraversalStrategy.NullObjectDescriptor, context);
					return;
				}
				base.Traverse<TContext>(name, value, visitor, context, path);
			}

			// Token: 0x04000808 RID: 2056
			private static readonly ObjectDescriptor NullObjectDescriptor = new ObjectDescriptor(null, typeof(object), typeof(object));
		}
	}
}
