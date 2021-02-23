using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ScoreModelImplementation
{
    class ScoreModel
    {
        /// <summary>
        /// Варинт выбора и его стоимость в оценке
        /// </summary>
        private struct Choice
        {

            public Choice(string name, float value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }

            public float Value { get; }

            public override string ToString() => $"{Name} ({Value})";
        }

        /// <summary>
        /// Имя правила и варианты выбора
        /// </summary>
        private struct Rule
        {
            public Rule(string name, float weight, Choice[] ruleChoices)
            {
                Name = name;
                Weight = weight;
                RuleChoices = ruleChoices;
            }

            public string Name { get; }

            public float Weight { get; }

            public Choice[] RuleChoices { get; }

            public override string ToString() => $"{Name} ({Weight}) [{RuleChoices.Length}]";
        }

        /// <summary>
        /// Вся модель
        /// </summary>
        private Rule[] Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Правила в формате: *Имя правила1*!*Вес*:*Имя Варианта1*_*Стоимость1*!*Имя Варианта2*_*Стоимость2* ... \n</param>
        public ScoreModel(string data)
        {
            //Делим на строки
            string[] rules_choices = data.Split('\n');
            //Делим на правила и варианты
            string[] rules = rules_choices.Select(s => s.Split(':')[0]).ToArray();
            string[] choices = rules_choices.Select(s => s.Split(':')[1]).ToArray();

            Data = new Rule[rules.Length];

            for (int i = 0; i < rules.Length; i++)
            {
                //Считываем варианты для правила
                string[] cho = choices[i].Split('!');
                int choicesN = choices[i].Split('!').Length;
                Choice[] actualChoices = new Choice[choicesN];
                for (int j = 0; j < choicesN; j++)
                {
                    string choiceName = cho[j].Split('_')[0];
                    float choiceValue = float.Parse(cho[j].Split('_')[1]);

                    actualChoices[j] = new Choice(choiceName, choiceValue);
                }

                string ruleName = rules[i].Split('!')[0];
                float ruleWeight= float.Parse(rules[i].Split('!')[1]);
                Data[i] = new Rule(ruleName, ruleWeight, actualChoices);
            }
        }

        /// <summary>
        /// Оценка по выбранным ответам
        /// </summary>
        /// <param name="choices"> Выбраные ответы </param>
        /// <returns> Пока суммарная оценка </returns>
        public float ReturnScore(int[] choices)
        {
            if (choices.Length != Data.Length)
                throw new Exception("Несоответсвие количества ответов количеству правил");

            float result = 0;

            for (int i = 0; i < choices.Length; i++)
                result += Data[i].Weight * Data[i].RuleChoices[choices[i]].Value;
            
            return result;
        }

        /// <summary>
        /// Возвращает смысл ответов
        /// </summary>
        /// <param name="choices"> Выбраные ответы </param>
        /// <returns>Вопросы и ответы клиента</returns>
        public string ReturnChoices(int[] choices)
        {
            string meaningChoices = "";

            for (int i = 0; i < choices.Length; i++)
                meaningChoices += Data[i].Name + ": " + Data[i].RuleChoices[choices[i]].Name + "\n";

            return meaningChoices;
        }
    }
}
