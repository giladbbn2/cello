
const FamilyTree = (function () {
    const CreateFamilyTree = async (motherId, motherName, fatherId, fatherName) => {
        const url = `${location.origin}/api/family_tree/create_family_tree?motherId=${motherId}&motherName=${motherName}&fatherId=${fatherId}&fatherName=${fatherName}`;

        try {
            const result = await xhrPromise("GET", url);

            const o = JSON.parse(result);

            return Promise.resolve(o.Result === true);
        } catch (error) {
            console.error(error);

            return Promise.reject(error);
        }
    };

    const Marry = async (id, otherId, otherName, isMale) => {
        const url = `${location.origin}/api/family_tree/marry?id=${id}&otherId=${otherId}&otherName=${otherName}&isMale=${(isMale ? "true" : "false")}`;

        try {
            const result = await xhrPromise("GET", url);

            const o = JSON.parse(result);

            return Promise.resolve(o.Result === true);
        } catch (error) {
            console.error(error);

            return Promise.reject(error);
        }
    };

    const HaveKid = async (motherId, fatherId, kidId, kidName, isKidMale) => {
        const url = `${location.origin}/api/family_tree/have_kids?motherId=${motherId}&fatherId=${fatherId}&kidId=${kidId}&kidName=${kidName}&isKidMale=${(isKidMale ? "true" : "false")}`;

        try {
            const result = await xhrPromise("GET", url);

            const o = JSON.parse(result);

            return Promise.resolve(o.Result === true);
        } catch (error) {
            console.error(error);

            return Promise.reject(error);
        }
    };

    const Divorce = async (motherId, fatherId, isCustodyWithMother) => {
        const url = `${location.origin}/api/family_tree/divorce?motherId=${motherId}&fatherId=${fatherId}&isCustodyWithMother=${isCustodyWithMother}`;

        try {
            const result = await xhrPromise("GET", url);

            const o = JSON.parse(result);

            return Promise.resolve(o.Result === true);
        } catch (error) {
            console.error(error);

            return Promise.reject(error);
        }
    };

    const Show = async () => {
        const url = `${location.origin}/api/family_tree/show`;

        try {
            const result = await xhrPromise("GET", url);

            const o = JSON.parse(result);

            return Promise.resolve(o.Result);
        } catch (error) {
            console.error(error);

            return Promise.reject(error);
        }
    };

    return {
        CreateFamilyTree,
        Marry,
        HaveKid,
        Divorce,
        Show
    };
})();