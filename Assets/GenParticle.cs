using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireWorkParticle;

    private int _genParticleAtOnce = 10;

    public void PlayFireWork()
    {
        StartCoroutine(PlayFireWorkParticleCor());
    }

    // {_genParticleAtOnce}回パーティクルを出すのを0.3秒間隔で3回行うコルーチン
    public IEnumerator PlayFireWorkParticleCor()
    {
        for (int i = 0; i < _genParticleAtOnce; i++)
        {
            GenerateFireWorkParticle();
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < _genParticleAtOnce; i++)
        {
            GenerateFireWorkParticle();
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < _genParticleAtOnce; i++)
        {
            GenerateFireWorkParticle();
        }

        yield break;
    }

    // パーティクルの生成
    private void GenerateFireWorkParticle()
    {
        // shape項目の取得
        ParticleSystem.ShapeModule shape = fireWorkParticle.shape;

        // 飛ばす角度の決定
        Vector3 rot = shape.position;
        rot.x = Random.Range(-6, 6);

        shape.position = rot;

        // パーティクルを飛ばす
        fireWorkParticle.Emit(10);
    }
}
