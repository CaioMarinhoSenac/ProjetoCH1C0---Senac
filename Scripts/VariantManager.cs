using Godot;
using System;
using System.Threading.Tasks;

public partial class VariantManager : Control
{
    [Export] public AnimatedSprite2D eyesAnimation;

    StateManager stateManager;

    string variantName;
    bool canSwitch = true;
    float cooldown = 2.0f; // DELAY ENTRE CADA TROCA DE VARIANTE, PARA DIMINUIR FRENETICIDADE

    public override void _Ready()
    {
        eyesAnimation.Play("FelizVariant");
    }

    public override void _Process(double delta)
    {
        SearchVariant();
    }

    public async void SwitchVariant(string variantName)
    {
        if (canSwitch)
        {
            eyesAnimation.Play(variantName);

            canSwitch = false;

            await Task.Delay((int)(cooldown * 1000)); // Convertendo segundos para milissegundos

            canSwitch = true;
        }
    }

    public void SearchVariant()
    {
        if(stateManager.currentState == stateManager.raivaState){
            variantName = "RaivaVariant";
        }
        else if(stateManager.currentState == stateManager.tristeState){
            variantName = "TristeVariant";
        }
        else if(stateManager.currentState == stateManager.felizState){
            variantName = "FelizVariant";
        }
        else if(stateManager.currentState == stateManager.coracaoState){
            variantName = "CoracaoVariant";
        }
        else if(stateManager.currentState == stateManager.medoState){
            variantName = "MedoVariant";
        }

        if(variantName != null)
        {
            SwitchVariant(variantName);
        }
    }
}
