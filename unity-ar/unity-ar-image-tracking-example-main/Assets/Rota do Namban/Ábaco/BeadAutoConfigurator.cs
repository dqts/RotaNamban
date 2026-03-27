using UnityEngine;
using System.Collections.Generic;

public class BeadAutoConfigurator : MonoBehaviour
{
    [Header("Contas Inferiores")]
    public BeadController[] contasDireita;
    public BeadController[] contasEsquerda;

    [Header("Configuração de Escada")]
    public float minY = -562.1f;    // fundo
    public float maxY = -39.7f;     // topo
    public int steps = 6;          // número de degraus

    void Start()
    {
        List<float> positionsY = CalcularDegraus(minY, maxY, steps);

        ConfigurarColuna(contasDireita, positionsY);
        ConfigurarColuna(contasEsquerda, positionsY);
    }

    List<float> CalcularDegraus(float minY, float maxY, int total)
    {
        List<float> list = new List<float>();
        float stepHeight = (maxY - minY) / (total - 1);

        for (int i = 0; i < total; i++)
        {
            float y = minY + (stepHeight * i);
            list.Add(y);
        }

        return list;
    }

    void ConfigurarColuna(BeadController[] contas, List<float> positionsY)
    {
        for (int i = 0; i < contas.Length; i++)
        {
            var bead = contas[i];
            bead.index = i;
            bead.group = contas;
            bead.positionsY = new List<float>(positionsY);

            // Define o passo mais próximo da posição atual
            float atualY = bead.GetY();
            float maisProximo = positionsY[0];
            int passoMaisProximo = 0;

            for (int j = 1; j < positionsY.Count; j++)
            {
                if (Mathf.Abs(atualY - positionsY[j]) < Mathf.Abs(atualY - maisProximo))
                {
                    maisProximo = positionsY[j];
                    passoMaisProximo = j;
                }
            }

            bead.currentStep = passoMaisProximo;
        }
    }
}
