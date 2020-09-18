using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App.Class
{ 
    public class FuzzyAlgorithm
    {
        /*
         * Algoritmo de Fuzzy | 08/06/2016
         * > Ismael Valentin
         * > Renan Ramos
         * 
         * Objetivo
         * --------
         * Algoritmo calcula o grau de pertinência conforme o universo, suporte e núcleo inseridos
         */

        private Double[] universe = new Double[2];
        private Double[] support = new Double[2];
        private Double[] core = new Double[2];
        private Double value;
        private Double relevance = 0;
        
        // Constructor
        public FuzzyAlgorithm()
        {
        }

        public void parameters(Double[] universe, Double[] support, Double[] core, Double value)
        {
            this.universe = universe;
            this.support = support;
            this.core = core;
            this.value = value;
            relevance = calcFuzzy();
        }

        // Metod GET
        public Double getRelevance
        {
            get { return relevance; }
        }

        // Fuzzy logical
        private Double calcFuzzy()
        {
            // generic
            if (value >= core[1] && value <= support[1])
            {
                return ((support[1] - value) / (support[1] - core[1]));
            }
            if (value >= support[0] && value <= core[0])
            {
                return ((value - support[0]) / (core[0] - support[0]));
            }
            // center
            if(value <= support[0] && value >= support[1])
            {
                return 0;
            }
            if(value >= core[0] && value <= core[1])
            {
                return 1;
            }
            if (support[1] == core[1])
            {
                // max
                if (value <= support[0])
                {
                    return 0;
                }
                if (value >= core[0])
                {
                    return 1;
                }
            }
            else
            {
                // min
                if (value >= support[1])
                {
                    return 0;
                }
                if (value <= core[1])
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
