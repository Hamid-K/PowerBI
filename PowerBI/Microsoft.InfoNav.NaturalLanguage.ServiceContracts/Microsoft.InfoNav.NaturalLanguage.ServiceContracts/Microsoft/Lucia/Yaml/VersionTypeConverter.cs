using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x02000023 RID: 35
	public sealed class VersionTypeConverter : IYamlTypeConverter
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00003333 File Offset: 0x00001533
		public bool Accepts(Type type)
		{
			return type == typeof(Version);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003345 File Offset: 0x00001545
		public object ReadYaml(IParser parser, Type type)
		{
			return Version.Parse(ParserExtensions.Consume<Scalar>(parser).Value);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003357 File Offset: 0x00001557
		public void WriteYaml(IEmitter emitter, object value, Type type)
		{
			emitter.Emit(new Scalar(value.ToString()));
		}
	}
}
