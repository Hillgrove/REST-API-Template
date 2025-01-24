const baseURL = "https://rest-api-template.azurewebsites.net/api/chairs"
// const baseURL = "http://localhost:5051/api/Chairs"

Vue.createApp({
    data() {
        return {
            allChairs: [],
            filteredChairs: [],

            addData: { model: "", maxWeight: 0, hasPillow: false },
            addMessage: "",

            updateData: { id: 0, model: "", maxWeight: 0, hasPillow: false },
            showUpdate: false,
            updateMessage: "",

            deleteId: 0,
            deleteMessage: "",

            sortColumn: "id",
            sortDirection: "asc",

            filterModel: null,
            filterMaxWeight: null
        }
    },

    async created() {
        this.getAllChairs(baseURL)
    },

    methods: {
        async getAllChairs(url) {
            try {
                response = await axios.get(url)
                this.allChairs = await response.data
                this.filteredChairs = [...this.allChairs]
            }

            catch(ex) {
                alert(ex.message)
            }
        },

        async addChair() {
            try {
                response = await axios.post(baseURL, this.addData)
                this.addMessage = "response " + response.status + " " + response.statusText
                this.getAllChairs(baseURL)

                this.addData = { model: "", maxWeight: 0, hasPillow: false }
            }

            catch(ex) {
                alert(ex.message)
            }
        },

        populateUpdateChair(chair) {
            this.updateData = { ...chair };
            this.updateMessage = "";
            this.showUpdate = true;
          },

        async updateChair() {
            this.showUpdate = false;

            const url = baseURL + "/" + this.updateData.id
            try {
                response = await axios.put(url, this.updateData)
                this.updateMessage = "response " + response.status + " " + response.statusText
                this.getAllChairs(baseURL)
            }

            catch(ex) {
                alert(ex.message)
            }
        },

        async deleteChair(deleteId) {
            const url = baseURL + "/" + deleteId
            try {
                response = await axios.delete(url)
                this.deleteMessage = "response " + response.status + " " + response.statusText
                this.getAllChairs(baseURL)
            }

            catch(ex) {
                alert(ex.message)
            }
        },

        sortBy(column) {
            if (this.sortColumn === column) {
                this.sortDirection = this.sortDirection === "asc" ? "desc" : "asc";
            }

            else {
                this.sortColumn = column;
                this.sortDirection = "asc";
            }

            this.filteredChairs.sort((a, b) => {
                if (a[column] < b[column]) return this.sortDirection === "asc" ? -1 : 1;
                if (a[column] > b[column]) return this.sortDirection === "asc" ? 1 : -1;
                return 0;
            });
        },

        filterByMaxWeight() {
            if (!this.filterMaxWeight) {
                this.filteredChairs = [...this.allChairs]
            }

            else {
                this.filteredChairs = this.allChairs.filter(chair => chair.maxWeight >= this.filterMaxWeight)
            }

            this.filterMaxWeight = ""
        },

        filterByModel(filterModel) {
            if (!filterModel) {
                this.filteredChairs = [...this.allChairs]
            }

            else {
                const lowercaseModel = filterModel.toLowerCase()
                this.filteredChairs = this.allChairs.filter(chair => chair.model.toLowerCase().includes(lowercaseModel))
            }

            this.filterModel = ""
        }
    }

}).mount("#app")