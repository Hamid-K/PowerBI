using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000190 RID: 400
	internal static class LsdlJsonValidator
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x000100F4 File Offset: 0x0000E2F4
		internal static JSchemaValidatingReader CreateValidatingReader(JsonReader reader)
		{
			return new JSchemaValidatingReader(reader)
			{
				Schema = LsdlJsonValidator.GetJsonSchema("lsdlschema.json")
			};
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001010C File Offset: 0x0000E30C
		internal static bool TryValidateJson(JObject schema, IDomainModelDiagnosticContext diagnosticContext)
		{
			Version version;
			LsdlVersion lsdlVersion;
			if (!LsdlVersion.TryParse(schema, out version) || !LsdlVersion.SupportedVersions.TryGetValue(version, out lsdlVersion))
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaVersionNotSupported());
				return false;
			}
			JSchema jsonSchema = LsdlJsonValidator.GetJsonSchema(lsdlVersion.JsonSchemaName);
			bool hasErrors = false;
			SchemaExtensions.Validate(schema, jsonSchema, delegate(object o, SchemaValidationEventArgs e)
			{
				LsdlJsonValidator.AddMostRelevantMessages(diagnosticContext, e.ValidationError);
				hasErrors = true;
			});
			return !hasErrors;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00010180 File Offset: 0x0000E380
		private static JSchema GetJsonSchema(string schemaFileName = "lsdlschema.json")
		{
			JSchema orAdd;
			if (!LsdlJsonValidator._schemas.TryGetValue(schemaFileName, out orAdd))
			{
				orAdd = LsdlJsonValidator._schemas.GetOrAdd(schemaFileName, (string s) => LsdlJsonValidator.LoadJsonSchema(s));
			}
			return orAdd;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000101C8 File Offset: 0x0000E3C8
		private static Stream GetJsonSchemaStream(string schemaFileName)
		{
			return typeof(LsdlJsonValidator).GetManifestResourceStream("LsdlSchema." + schemaFileName);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000101E4 File Offset: 0x0000E3E4
		private static JSchema LoadJsonSchema(string schemaFileName)
		{
			JSchema jschema;
			using (JsonReader jsonReader = JsonReaderFactory.Create(LsdlJsonValidator.GetJsonSchemaStream(schemaFileName), false))
			{
				JSchemaReaderSettings jschemaReaderSettings = new JSchemaReaderSettings
				{
					Validators = new List<JsonValidator>
					{
						LsdlJsonValidator.LanguageFormatValidator.Instance,
						LsdlJsonValidator.IdentifierValidator.Instance
					},
					Resolver = new LsdlJsonValidator.LsdlSchemaResolver()
				};
				jschema = JSchema.Load(jsonReader, jschemaReaderSettings);
			}
			return jschema;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00010258 File Offset: 0x0000E458
		private static void AddMostRelevantMessages(IDomainModelDiagnosticContext diagnosticContext, ValidationError rootError)
		{
			if (LsdlJsonValidator.ShouldFindMostRelevantChildError(rootError))
			{
				global::System.ValueTuple<ValidationError, IList<ValidationError>, int, int> valueTuple = LsdlJsonValidator.FindMostRelevantError(rootError.ChildErrors, 0);
				using (IEnumerator<ValidationError> enumerator = valueTuple.Item2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ValidationError validationError = enumerator.Current;
						if (valueTuple.Item1 == null || validationError.SchemaId == valueTuple.Item1.SchemaId)
						{
							LsdlJsonValidator.RegisterValidationMessage(diagnosticContext, validationError);
						}
					}
					return;
				}
			}
			if (LsdlJsonValidator.ShouldRecurseAllChildErrors(rootError))
			{
				using (IEnumerator<ValidationError> enumerator = rootError.ChildErrors.Reverse<ValidationError>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ValidationError validationError2 = enumerator.Current;
						LsdlJsonValidator.AddMostRelevantMessages(diagnosticContext, validationError2);
					}
					return;
				}
			}
			LsdlJsonValidator.RegisterValidationMessage(diagnosticContext, rootError);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001032C File Offset: 0x0000E52C
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "MostRelevantError", "PeerErrors", "Depth", "PathLength" })]
		private static global::System.ValueTuple<ValidationError, IList<ValidationError>, int, int> FindMostRelevantError(IList<ValidationError> errors, int depth)
		{
			ValidationError validationError = null;
			IList<ValidationError> list = null;
			int num = 0;
			int num2 = 0;
			foreach (ValidationError validationError2 in errors)
			{
				if (LsdlJsonValidator.ShouldFindMostRelevantChildError(validationError2))
				{
					global::System.ValueTuple<ValidationError, IList<ValidationError>, int, int> valueTuple = LsdlJsonValidator.FindMostRelevantError(validationError2.ChildErrors, depth + 1);
					if (valueTuple.Item3 >= num && valueTuple.Item4 >= num2)
					{
						validationError = valueTuple.Item1;
						list = valueTuple.Item2;
						num = valueTuple.Item3;
						num2 = valueTuple.Item4;
					}
				}
				else
				{
					if (LsdlJsonValidator.ShouldRecurseAllChildErrors(validationError2))
					{
						using (IEnumerator<ValidationError> enumerator2 = validationError2.ChildErrors.Reverse<ValidationError>().GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ValidationError validationError3 = enumerator2.Current;
								int num3 = depth + 1;
								if (num3 >= num && validationError3.Path.Length >= num2)
								{
									validationError = null;
									list = validationError2.ChildErrors;
									num = num3;
									num2 = validationError3.Path.Length;
								}
							}
							continue;
						}
					}
					if (depth >= num && validationError2.Path.Length >= num2)
					{
						validationError = validationError2;
						list = errors;
						num = depth;
						num2 = validationError2.Path.Length;
					}
				}
			}
			return new global::System.ValueTuple<ValidationError, IList<ValidationError>, int, int>(validationError, list, num, num2);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000104A0 File Offset: 0x0000E6A0
		private static bool ShouldFindMostRelevantChildError(ValidationError error)
		{
			return (error.ErrorType == 21 || error.ErrorType == 20) && !error.ChildErrors.IsNullOrEmptyCollection<ValidationError>();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000104C6 File Offset: 0x0000E6C6
		private static bool ShouldRecurseAllChildErrors(ValidationError error)
		{
			return error.ErrorType == 19 && !error.ChildErrors.IsNullOrEmptyCollection<ValidationError>();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000104E2 File Offset: 0x0000E6E2
		private static void RegisterValidationMessage(IDomainModelDiagnosticContext diagnosticContext, ValidationError error)
		{
			diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaSyntacticValidationErrorJson(error.Message, error.Path, error));
		}

		// Token: 0x04000700 RID: 1792
		private static readonly ConcurrentDictionary<string, JSchema> _schemas = new ConcurrentDictionary<string, JSchema>();

		// Token: 0x02000232 RID: 562
		private sealed class LsdlSchemaResolver : JSchemaResolver
		{
			// Token: 0x06000BFF RID: 3071 RVA: 0x00018068 File Offset: 0x00016268
			public override Stream GetSchemaResource(ResolveSchemaContext context, SchemaReference reference)
			{
				return LsdlJsonValidator.GetJsonSchemaStream(reference.BaseUri.ToString());
			}
		}

		// Token: 0x02000233 RID: 563
		private sealed class LanguageFormatValidator : JsonValidator
		{
			// Token: 0x1700034C RID: 844
			// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00018082 File Offset: 0x00016282
			internal static LsdlJsonValidator.LanguageFormatValidator Instance { get; } = new LsdlJsonValidator.LanguageFormatValidator();

			// Token: 0x06000C02 RID: 3074 RVA: 0x00018089 File Offset: 0x00016289
			private LanguageFormatValidator()
			{
			}

			// Token: 0x06000C03 RID: 3075 RVA: 0x00018094 File Offset: 0x00016294
			public override void Validate(JToken value, JsonValidatorContext context)
			{
				if (value.Type == JTokenType.String)
				{
					string text = value.ToString();
					LanguageIdentifier languageIdentifier;
					if (string.IsNullOrEmpty(text) || !LanguageIdentifierUtil.TryAsLanguageIdentifier(text, out languageIdentifier))
					{
						context.RaiseError("String '" + text + "' is not a valid language name.");
					}
				}
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x000180D9 File Offset: 0x000162D9
			public override bool CanValidate(JSchema schema)
			{
				return schema.Format == "language";
			}
		}

		// Token: 0x02000234 RID: 564
		private sealed class IdentifierValidator : JsonValidator
		{
			// Token: 0x1700034D RID: 845
			// (get) Token: 0x06000C06 RID: 3078 RVA: 0x000180F7 File Offset: 0x000162F7
			internal static LsdlJsonValidator.IdentifierValidator Instance { get; } = new LsdlJsonValidator.IdentifierValidator();

			// Token: 0x06000C07 RID: 3079 RVA: 0x000180FE File Offset: 0x000162FE
			private IdentifierValidator()
			{
			}

			// Token: 0x06000C08 RID: 3080 RVA: 0x00018108 File Offset: 0x00016308
			public override void Validate(JToken value, JsonValidatorContext context)
			{
				if (value.Type == JTokenType.String)
				{
					LsdlJsonValidator.IdentifierValidator.RaiseErrorIfNeeded(value.ToString(), context);
					return;
				}
				if (value.Type == JTokenType.Object)
				{
					foreach (JProperty jproperty in value.Children<JProperty>())
					{
						LsdlJsonValidator.IdentifierValidator.RaiseErrorIfNeeded(jproperty.Name, context);
					}
				}
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x0001817C File Offset: 0x0001637C
			public override bool CanValidate(JSchema schema)
			{
				return schema.Format == "identifier" || schema.Format == "identifier-properties";
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x000181A2 File Offset: 0x000163A2
			private static void RaiseErrorIfNeeded(string identifier, JsonValidatorContext context)
			{
				if (!LsdlJsonValidator.IdentifierValidator.IsValidIdentifier(identifier))
				{
					context.RaiseError("The specified identifier '" + identifier + "' is not valid. LSDL identifiers must consist of one or more dot-separated CLS-compliant identifiers.");
				}
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x000181C2 File Offset: 0x000163C2
			private static bool IsValidIdentifier(string identifier)
			{
				IEnumerable<string> enumerable = identifier.Split(new char[] { '.' });
				Func<string, bool> func;
				if ((func = LsdlJsonValidator.IdentifierValidator.<>O.<0>__IsClsCompliantIdentifier) == null)
				{
					func = (LsdlJsonValidator.IdentifierValidator.<>O.<0>__IsClsCompliantIdentifier = new Func<string, bool>(StringUtil.IsClsCompliantIdentifier));
				}
				return enumerable.All(func);
			}

			// Token: 0x0200025D RID: 605
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040009AB RID: 2475
				public static Func<string, bool> <0>__IsClsCompliantIdentifier;
			}
		}
	}
}
