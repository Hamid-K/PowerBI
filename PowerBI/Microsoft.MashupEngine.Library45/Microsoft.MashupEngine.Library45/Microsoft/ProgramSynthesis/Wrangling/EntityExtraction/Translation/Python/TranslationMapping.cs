using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Translation.Python
{
	// Token: 0x020001B2 RID: 434
	public static class TranslationMapping
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001C398 File Offset: 0x0001A598
		private static Dictionary<Type, IReadOnlyList<string>> TypeToResourceName
		{
			get
			{
				return TranslationMapping.TypeToResourceNameLazy.Value;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		private static Dictionary<Type, IReadOnlyList<string>> BuildTypeToResourceNameMap()
		{
			Dictionary<Type, IReadOnlyList<string>> dictionary = new Dictionary<Type, IReadOnlyList<string>>();
			Type typeFromHandle = typeof(CurrencyTokenizer);
			dictionary[typeFromHandle] = new string[] { "numeric_tokenizer.py", "currency_tokenizer.py" };
			Type typeFromHandle2 = typeof(DashedNumbersTokenizer);
			dictionary[typeFromHandle2] = new string[] { "dashed_numbers_tokenizer.py" };
			Type typeFromHandle3 = typeof(DateTokenizer);
			dictionary[typeFromHandle3] = new string[] { "date_tokenizer.py" };
			Type typeFromHandle4 = typeof(TimeTokenizer);
			dictionary[typeFromHandle4] = new string[] { "time_tokenizer.py" };
			Type typeFromHandle5 = typeof(DomainNameTokenizer);
			dictionary[typeFromHandle5] = new string[] { "domain_name_tokenizer.py" };
			Type typeFromHandle6 = typeof(EmailTokenizer);
			dictionary[typeFromHandle6] = new string[] { "email_tokenizer.py" };
			Type typeFromHandle7 = typeof(IpAddressTokenizer);
			dictionary[typeFromHandle7] = new string[] { "ip_address_tokenizer.py" };
			Type typeFromHandle8 = typeof(MacAddressTokenizer);
			dictionary[typeFromHandle8] = new string[] { "mac_address_tokenizer.py" };
			Type typeFromHandle9 = typeof(NumericTokenizer);
			dictionary[typeFromHandle9] = new string[] { "numeric_tokenizer.py" };
			Type typeFromHandle10 = typeof(PathTokenizer);
			dictionary[typeFromHandle10] = new string[] { "path_tokenizer.py" };
			Type typeFromHandle11 = typeof(PhoneNumberTokenizer);
			dictionary[typeFromHandle11] = new string[] { "phone_number_tokenizer.py" };
			Type typeFromHandle12 = typeof(UrlTokenizer);
			dictionary[typeFromHandle12] = new string[] { "url_tokenizer.py" };
			return dictionary;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001C554 File Offset: 0x0001A754
		public static List<KeyValuePair<string, string>> GetSourceCodeForType(Type type)
		{
			Assembly assembly = typeof(TranslationMapping).GetTypeInfo().Assembly;
			string prefix = assembly.GetName().Name + ".Wrangling.EntityExtraction.Translation.Python";
			IReadOnlyList<string> readOnlyList;
			if (!TranslationMapping.TypeToResourceName.TryGetValue(type, out readOnlyList))
			{
				return null;
			}
			return TranslationMapping.CommonResources.Concat(readOnlyList).Select(delegate(string name)
			{
				string text = AssemblyResourceUtils.LoadResourceFromAssembly(assembly, prefix + "." + name);
				return new KeyValuePair<string, string>(name, text);
			}).ToList<KeyValuePair<string, string>>();
		}

		// Token: 0x04000494 RID: 1172
		private static readonly Lazy<Dictionary<Type, IReadOnlyList<string>>> TypeToResourceNameLazy = new Lazy<Dictionary<Type, IReadOnlyList<string>>>(new Func<Dictionary<Type, IReadOnlyList<string>>>(TranslationMapping.BuildTypeToResourceNameMap));

		// Token: 0x04000495 RID: 1173
		private static readonly List<string> CommonResources = new List<string> { "entity_extraction_common.py" };
	}
}
