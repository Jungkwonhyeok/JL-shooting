using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    public int bgmchannels;
    AudioSource[] bgmPlayers;
    int bgmchannelIndex;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int sfxchannels;
    AudioSource[] sfxPlayers;
    int sfxchannelIndex;

    public enum Bgm {Lobby, Game, Boss}
    public enum Sfx { SBullet, LBullet, ButtonClick, Hit, Destroy }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayers = new AudioSource[bgmchannels];

        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            bgmPlayers[index] = bgmObject.AddComponent<AudioSource>();
            bgmPlayers[index].playOnAwake = false;
            bgmPlayers[index].volume = bgmVolume;
        }

        //효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxchannels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        // 기존에 재생 중인 모든 BGM 중지
        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            if (bgmPlayers[index].isPlaying)
                bgmPlayers[index].Stop();
        }

        // 새 BGM 재생
        int newindex = (int)bgm;
        bgmPlayers[0].clip = bgmClips[newindex];
        bgmPlayers[0].loop = true;
        bgmPlayers[0].volume = bgmVolume;
        bgmPlayers[0].Play();

        bgmchannelIndex = 0; // 현재 사용 중인 채널 인덱스 갱신
    }


    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + sfxchannelIndex) % sfxPlayers.Length; //채널 개수만큼 순회하도록 해주는 채널 인덱스

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            sfxchannelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
    public void StopBgm()
    {
        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            if (bgmPlayers[index].isPlaying)
                bgmPlayers[index].Stop();
        }
    }

}