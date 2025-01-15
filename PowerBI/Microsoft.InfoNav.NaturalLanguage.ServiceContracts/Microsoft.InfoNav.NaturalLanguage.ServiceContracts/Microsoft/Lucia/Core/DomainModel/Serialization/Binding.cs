using System;
using Microsoft.Lucia.Json;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200019A RID: 410
	public abstract class Binding : ICustomSerializationOptions, IEquatable<Binding>
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00010C67 File Offset: 0x0000EE67
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x06000849 RID: 2121
		public abstract bool Equals(Binding other);

		// Token: 0x0600084A RID: 2122
		protected abstract int GetHashCodeCore();

		// Token: 0x0600084B RID: 2123 RVA: 0x00010C6A File Offset: 0x0000EE6A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Binding);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00010C78 File Offset: 0x0000EE78
		public sealed override int GetHashCode()
		{
			return this.GetHashCodeCore();
		}

		// Token: 0x0200023C RID: 572
		internal sealed class AnyBindingConverter : JTokenConverterBase<Binding, JObject>
		{
			// Token: 0x06000C41 RID: 3137 RVA: 0x00018F68 File Offset: 0x00017168
			protected override Binding Create(Type objectType, JObject obj, JsonSerializer serializer)
			{
				LsdlJsonSerializer.MapBindingPropertiesToCanonicalForm(obj, serializer);
				if (obj.HasProperty("ConceptualProperty"))
				{
					return serializer.Deserialize<ConceptualPropertyBinding>(obj.CreateReader());
				}
				if (obj.HasProperty("HierarchyLevel"))
				{
					return serializer.Deserialize<HierarchyLevelBinding>(obj.CreateReader());
				}
				if (obj.HasProperty("Hierarchy"))
				{
					return serializer.Deserialize<HierarchyBinding>(obj.CreateReader());
				}
				return serializer.Deserialize<ConceptualEntityBinding>(obj.CreateReader());
			}
		}
	}
}
