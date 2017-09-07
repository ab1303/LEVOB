using System;
using System.Collections;
using System.Collections.Generic;
using Glossary.Services.DTO;

namespace Glossary.WebApi.Models.Glossary
{

    public class GlossaryResponse : BaseApiResponse
    {
        public GlossaryDto Glossary { get; set; }

    }

    public class GlossaryListResponse : BaseApiResponse
    {
        public IEnumerable<GlossaryDto> Glossaries { get; set; }

    }
}